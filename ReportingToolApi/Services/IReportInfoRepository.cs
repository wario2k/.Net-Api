using ReportingToolApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingToolApi.Services
{
    public interface IReportInfoRepository
    {
        bool ReportExists(int reportId);
        IEnumerable<Report> GetReports();
        Report GetReport(int reportId);
        void AddReport(Report report);
        void DeleteReport(Report report);
        bool Save();
    }
}
