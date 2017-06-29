﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;
using DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities;
using Utilities.Calculators;
using Utilities.Mappers;

namespace GSACapitalAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICalculator<Tuple<List<Capital>, List<ProfitNLoss>, string>, List<CompoundDailyReturnDTO>>, CompoundDailyReturnCalculator>();
            services.AddTransient<ICalculator<List<ProfitNLoss>, List<CumulativePNL>>, CumulativePnLCalculator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            LoadFiles(env);
        }

        public void LoadFiles(IHostingEnvironment env)
        {
            var dataLoader = new DataLoader();
            var capitalPath = Path.Combine(env.ContentRootPath, "AppData", "capital.csv");
            var capital = dataLoader.Load(capitalPath);

            var capitalMapper = new CapitalMapper();
            var capitalDTOs = capitalMapper.ToDTO(capital[0], capital.Skip(1).ToList());


            var pnlPath = Path.Combine(env.ContentRootPath, "AppData", "pnl.csv");
            var pnls = dataLoader.Load(pnlPath);
            var pnlMapper = new ProfitNLossMapper();
            var pnlDTOs = pnlMapper.ToDTO(pnls[0], pnls.Skip(1).ToList());

            var propertiesth = Path.Combine(env.ContentRootPath, "AppData", "properties.csv");
            var properties = dataLoader.Load(propertiesth);
            var propertiesMapper = new StrategyMapper();
            var propertiesDTO = propertiesMapper.ToDTO(properties.Skip(1).ToList());

            var uow = new UnitOfWork();

            var strategiesRepo = uow.GetRepository<Strategy>();

            var pnlRepo = uow.GetRepository<ProfitNLoss>();

            var capitalRepo = uow.GetRepository<Capital>();

            var count = strategiesRepo.GetQuery().Count();

            if (count == 0)
            {
                List<Strategy> strategies;

                foreach (var property in propertiesDTO)
                {
                    var strategy = new Strategy()
                    {
                        Region = property.Region,
                        Name = property.Name
                    };

                    strategiesRepo.Create(strategy);
                }

                uow.Commit();

                strategies = strategiesRepo.GetQuery().ToList();

                foreach (var capitalDTO in capitalDTOs)
                {
                    var capitalEntity = new Capital();
                    capitalEntity.ID = Guid.NewGuid();
                    capitalEntity.Value = capitalDTO.Value;
                    capitalEntity.Date = capitalDTO.Date;
                    capitalEntity.Strategy = strategies.Where(x => x.Name == capitalDTO.Name).First();
                    capitalRepo.Create(capitalEntity);
                }

                foreach (var pnlDTO in pnlDTOs)
                {
                    var capitalEntity = new ProfitNLoss();
                    capitalEntity.ID = Guid.NewGuid();
                    capitalEntity.Value = pnlDTO.Value;
                    capitalEntity.Date = pnlDTO.Date;
                    capitalEntity.Strategy = strategies.Where(x => x.Name == pnlDTO.Strategy).First();
                    pnlRepo.Create(capitalEntity);
                }
                uow.Commit();
            }

        }
    }
}
