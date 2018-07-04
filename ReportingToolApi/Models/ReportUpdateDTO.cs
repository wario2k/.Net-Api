using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingToolApi.Models
{
    public class ReportUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string TestName { get; set; }
        [MaxLength(6)] //pass or fail
        public string Result { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
