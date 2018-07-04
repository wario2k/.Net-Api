using ReportingToolApi.Models;
using System.Collections.Generic;

namespace ReportingToolApi
{
    public class ReportDataStore
    {
        public static ReportDataStore Current { get; } = new ReportDataStore(); //auto property init immutable
        public List<ReportDTO> Reports { get; set; }

        public ReportDataStore()
        {
            Reports = new List<ReportDTO>()
            {
                new ReportDTO()
                {
                    Id = 1,
                    TestName = "test1",
                    Result = "pass",
                    AdditionalInfo = "link"

                },

                 new ReportDTO()
                {
                    Id = 2,
                    TestName = "test2",
                    Result = "fail",
                    AdditionalInfo = "google.com"

                }
            };
        }
    }
}
