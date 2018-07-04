using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReportingToolApi.Models;
using ReportingToolApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingToolApi.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        private IReportInfoRepository _reportInfoRepo;

        public ReportsController(IReportInfoRepository report)
        {
            _reportInfoRepo = report;
        }

        [HttpGet()]
        public IActionResult GetReports()
        {
            var reports = _reportInfoRepo.GetReports();

            var results = Mapper.Map<IEnumerable<ReportDTO>>(reports);
            
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetReport(int id)
        {
            var report = _reportInfoRepo.GetReport(id);
            if (report == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<ReportDTO>(report);
            return Ok(result);

        }

        [HttpPost()]
        public IActionResult CreateReport([FromBody] ReportCreationDTO report)
        {
            if (report == null) //if we have nothing in our post body return bad request
            {
                return BadRequest();
            }

            //checks if the rules implemented in the Dto were adhered to.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!(report.Result.ToLower() == "pass" || report.Result.ToLower() == "fail"))
            {
                return BadRequest("Invalid Results!");
            }

            //create a new report
            var newReport = Mapper.Map<Entities.Report>(report);

            _reportInfoRepo.AddReport(newReport);

            if (!_reportInfoRepo.Save())
            {
                return StatusCode(500, "An error was encountered handling your request!");
            }

            var createdReport = Mapper.Map<Models.ReportCreationDTO>(newReport);

            return Ok(createdReport);

        }

        [HttpPut("{id}")] //we're updating a report using its Id 
        public IActionResult UpdateReport(int id, [FromBody] ReportUpdateDTO updatedReport)
        {

            if (updatedReport == null) //if we have nothing in our post body return bad request
            {
                return BadRequest();
            }

            //checks if the rules implemented in the Dto were adhered to.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reportInfoRepo.ReportExists(id))
            {
                return NotFound();
            }
            var reportToUpdate = _reportInfoRepo.GetReport(id);
            if (reportToUpdate == null)
            {
                return NotFound();
            }

            Mapper.Map(updatedReport, reportToUpdate);

            if (!_reportInfoRepo.Save())
            {
                return StatusCode(500, "An error was encountered handling your request!");
            }

            return Accepted();
        }


        [HttpPatch("{id}")]
        public IActionResult PatchReport(int id, [FromBody] JsonPatchDocument<ReportUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
            {
                return NotFound();
            }

            if (!_reportInfoRepo.ReportExists(id))
            {
                return NotFound();
            }
            var reportEntity = _reportInfoRepo.GetReport(id);
            var patchReport = Mapper.Map<ReportUpdateDTO>(reportEntity);

            patchDoc.ApplyTo(patchReport, ModelState);

            //for patch
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(patchReport);
            //for model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //update values according to patch doc
            Mapper.Map(patchReport, reportEntity);

            if (!_reportInfoRepo.Save())
            {
                return StatusCode(500, "An error was encountered handling your request!");
            }

            return Accepted();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReport(int id)
        {
           if(!_reportInfoRepo.ReportExists(id))
            {
                return NotFound();
            }

            var reportToDelete = _reportInfoRepo.GetReport(id);
            _reportInfoRepo.DeleteReport(reportToDelete);

            if (!_reportInfoRepo.Save())
            {
                return StatusCode(500, "An error was encountered handling your request!");
            }
            
            return NoContent();
        }
    }
}
