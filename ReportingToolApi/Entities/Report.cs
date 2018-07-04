using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingToolApi.Entities
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //primary key
        [Required]
        [MaxLength(50)]
        public string TestName { get; set; }
        [Required]
        [MaxLength(5)]
        public string Result { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
