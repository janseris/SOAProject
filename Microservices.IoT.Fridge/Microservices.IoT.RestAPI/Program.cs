using System.Reflection;

using App.Metrics.Formatters.Json;
using App.Metrics.Formatters.Prometheus;

using Microservices.IoT.API.Services.Events;
using Microservices.IoT.API.Services.FoodItems;
using Microservices.IoT.API.Services.Fridges;
using Microservices.IoT.Data.DAOs.Events;
using Microservices.IoT.Data.DAOs.FoodItems;
using Microservices.IoT.Data.DAOs.Fridges;
using Microservices.IoT.Data.Models;
using Microservices.IoT.ManagementConsole.RestAPI.Services;
using Microservices.IoT.Services.Events;
using Microservices.IoT.Services.FoodItems;
using Microservices.IoT.Services.Fridges;

using Microsoft.EntityFrameworkCore;

using SwaggerUIAuthMiddleware;

namespace Microservices.IoT.ManagementConsole.RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //https://stackoverflow.com/questions/69390676/how-to-use-appsettings-json-in-asp-net-core-6-program-cs-file#answer-70771643
            var settings = builder.Configuration; //read appsettings.json

            // Add services to the container.

            DISetup.ConfigureServices(settings, builder.Services);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                //xml documentation generating must be enabled in Project -> Properties -> Build
                //when <response> tags are used in functions documentation in controllers,
                //use case description is displayed on each status code declared by annotations
                //also method description is displayed in the Swagger UI
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; //project name.xml (default)
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            #region monitoring

            //registers IMetrics interface instance in the application, this 1 instance is then used to measure all metrics
            builder.Services.AddMetrics();

            #endregion


            var app = builder.Build();


            #region add Swagger UI access control

            var swaggerAuthOptions = settings.GetSection("SwaggerAuthOptions").Get<SwaggerUIAuthenticationMiddlewareOptions>();

            //toggle between Development/Production environment in:
            //Project properties -> Debug -> Open Debug launch profiles UI -> Environment variables -> ASPNETCORE_ENVIRONMENT 
            if (app.Environment.IsProduction())
            {
                //https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/384#issuecomment-410117400
                app.UseSwaggerAuthorized(swaggerAuthOptions);
            }

            #endregion

            // Configure the HTTP request pipeline.
            app.UseSwaggerUI(options =>
                    {
                        //displays friendly function name in the API method description, this is useful for generating a C# API client through VS
                        options.DisplayOperationId();
                    });
            app.UseSwagger();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            };

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            var sensorsManagementService = app.Services.GetRequiredService<SensorsManagementService>();
            var applicationStoppingAction = new Action(() => sensorsManagementService.KillAllMicroservices());
            app.Lifetime.ApplicationStopping.Register(applicationStoppingAction);

            #region monitoring

            //types of metrics: gauge = up and down, counter = up


            //here it was supposed to be app.UseMetricsWebTracking
            app.UseMetricsRequestTrackingMiddleware(); //maybe?

            //prometheus needs text formatter
            var metricsFormatter = new MetricsPrometheusTextOutputFormatter();
            app.UseMetricsEndpoint(metricsFormatter);

            #endregion

            app.Run();
        }
    }
}