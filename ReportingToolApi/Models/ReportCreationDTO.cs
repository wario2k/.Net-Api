using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingToolApi.Models
{
    public class ReportCreationDTO
    {
        [Required]
        [MaxLength(50)]
        public string TestName { get; set; }
        [MaxLength(6)] //expecting only pass or fail as valid inputs 
        public string Result { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
