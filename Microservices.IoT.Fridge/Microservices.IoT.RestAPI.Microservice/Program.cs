using System.Diagnostics;
using System.Reflection;

using Microservices.IoT.Data.DAOs;

namespace Microservices.IoT.Sensor.RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Arguments ({args.Length}):");
            foreach(var arg in args)
            {
                Console.WriteLine(arg);
            }

            if(args.Length != 2)
            {
                Console.Error.WriteLine("Wrong argument count");
            }

            var name = args[0];
            int port = int.Parse(args[1]);
            var config = new ApplicationConfig
            {
                Name = name,
                Port = port
            };

            Console.Title = $"Microservice for fridge '{config.Name}' running on port {config.Port}";


            var builder = WebApplication.CreateBuilder(args);

            //https://stackoverflow.com/questions/69390676/how-to-use-appsettings-json-in-asp-net-core-6-program-cs-file#answer-70771643
            var settings = builder.Configuration; //read appsettings.json

            // Add services to the container.

            DISetup.ConfigureServices(settings, builder.Services);

            #region custom logic

            builder.Services.AddSingleton(config); //add the instance to be available in the DI Cotainer

            #endregion

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

            var app = builder.Build();


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
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #region custom logic

            var startupAction = new Action(() => OnApplicationStarted(app.Services));
            app.Lifetime.ApplicationStarted.Register(startupAction);

            string processAddress = $"https://localhost:{config.Port}";
            #endregion

            app.Run(processAddress);
            app.Run();
        }

        private static void OnApplicationStarted(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetRequiredService<ApplicationConfig>();
            Console.WriteLine($"Application {config.Name} started on port {config.Port}");

            Stopwatch s = Stopwatch.StartNew();

            var warmUpDAO = serviceProvider.GetRequiredService<WarmUpDAO>();
            warmUpDAO.WarmUp();

            s.Stop();
            Console.WriteLine($"Application {config.Name} (port {config.Port}) ready in {s.ElapsedMilliseconds} ms");
        }
    }
}