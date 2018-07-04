using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingToolApi.Entities
{
    public class ReportContext : DbContext
    {

        public DbSet<Report> Reports { get; set; }

        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {
            Database.Migrate(); // if db does not exist creates new db 
        }
    }
}
