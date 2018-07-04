using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingToolApi.Models
{
    public class ReportDTO
    {
        public int Id { get; set; } //key
        public string TestName { get; set; }
        public string Result { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
