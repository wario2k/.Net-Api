using System.Collections.Generic;
using System.Linq;
using ReportingToolApi.Entities;

namespace ReportingToolApi.Services
{
    public class ReportInfoRepository : IReportInfoRepository
    {
        private ReportContext _context;

        public ReportInfoRepository(ReportContext context)
        {
            _context = context; //service injection 
        }
        IEnumerable<Report> IReportInfoRepository.GetReports()
        {
            return _context.Reports; 
        }

        Report IReportInfoRepository.GetReport(int reportId)
        {
            return _context.Reports.Where(r => r.Id == reportId).FirstOrDefault();
        }

        public bool ReportExists(int reportId)
        {
            return _context.Reports.Any(c => c.Id == reportId);
        }

        public bool Save()
        {
            return(_context.SaveChanges() >= 0);
        }

        public void AddReport(Report report)
        {
            var reports = _context.Reports;
            reports.Add(report);
        }

        public void DeleteReport(Report report)
        {
            _context.Reports.Remove(report);
        }
    }
}
