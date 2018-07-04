using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportingToolApi.Entities;
using ReportingToolApi.Services;

namespace ReportingToolApi
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }
        //set up for config files
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters
                                          .Add(new XmlDataContractSerializerOutputFormatter())); //handles xml requests

            //var connectionString = @"Server=(localdb)\mssqllocaldb;Database=ReportInfoDB;Trusted_Connection=True;";
            var connectionString = Configuration.GetValue<string>("connectionString");
            //For DB context injection
            services.AddDbContext<ReportContext>(o=> o.UseSqlServer(connectionString));

            services.AddScoped<IReportInfoRepository, ReportInfoRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //handles bad status codes 
            app.UseStatusCodePages();//doesn't show blank screen when bad requests are made

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Report, Models.ReportDTO>();
                cfg.CreateMap<Models.ReportCreationDTO, Entities.Report>();
                cfg.CreateMap<Models.ReportUpdateDTO, Entities.Report>();
                cfg.CreateMap<Entities.Report, Models.ReportUpdateDTO>();
            });

            app.UseMvc();
        }
    }
}
