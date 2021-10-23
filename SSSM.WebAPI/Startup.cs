using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SSSM.Api;
using SSSM.Repositories;
using SSSM.Services;
using System;

namespace SSSM.WebAPI
{
    public class Startup
    {
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        if (CurrentEnvironment.IsDevelopment())
                        {
                            builder.AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
                        }
                        else
                        {
                            var uiEndpoint = Environment.GetEnvironmentVariable("UI_ENDPOINT");

                            if (string.IsNullOrEmpty(uiEndpoint))
                            {
                                throw new Exception("Environment variable not found: UI_ENDPOINT");
                            }

                            builder.WithOrigins(uiEndpoint)
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                        }
                    });
            });

            services.AddSingleton<IStockMarketRepository, StockMarketRepository>();
            services.AddSingleton<IStockMarketService, StockMarketService>();
            services.AddControllers(x => x.Filters.Add<ExceptionFilter>())
                .AddNewtonsoftJson((x) =>
                {
                    x.SerializerSettings.Converters.Add(new DateFormatConverter("yyyy-MM-dd HH:mm:ss"));
                }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
