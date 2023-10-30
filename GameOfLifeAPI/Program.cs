using System.Reflection;
using System.Text.Json;
using System.Text;
using FluentAssertions.Common;
using GameOfLife.API.Controllers;
using GameOfLife.Business;
using GameOfLife.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.PlatformAbstractions;

namespace GameOfLife.Business.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Dentro del método ConfigureServices en Startup.cs
            builder.Services.AddScoped<GameOfLife>(provider =>
            {
                var boardRepository = new FileSystemBoardRepository(@"\GameOfLife.API");
                return new GameOfLife(boardRepository);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Game Of Life API v1",
                    Version = "v1",
                    Description = "A 1.0 game version of game of life API",
                });

                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Game Of Life API v2",
                    Version = "v2",
                    Description = "A 2.0 game version of game of life API",
                });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (docName == "v1" && apiDesc.RelativePath.Contains("v1"))
                    {
                        return true;
                    }
                    if (docName == "v2" && apiDesc.RelativePath.Contains("v2"))
                    {
                        return true;
                    }
                    return false;
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)) + ".xml";
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }

            });


            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            
            string storagePath = @"\GameOfLife.API"; // Reemplaza con la ruta real

            builder.Services.AddHealthChecks()
                .AddCheck<GamePersistenceHealthCheck>("game_persistence",
                tags: new[] { "persistence" },
                failureStatus: HealthStatus.Unhealthy);



            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GameOfLife API v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "GameOfLife API v2");
                });
            }


            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = WriteResponse
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", healthReport.Status.ToString());
                jsonWriter.WriteStartObject("results");

                foreach (var healthReportEntry in healthReport.Entries)
                {
                    jsonWriter.WriteStartObject(healthReportEntry.Key);
                    jsonWriter.WriteString("status",
                        healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteString("description",
                        healthReportEntry.Value.Description);
                    jsonWriter.WriteStartObject("data");

                    foreach (var item in healthReportEntry.Value.Data)
                    {
                        jsonWriter.WritePropertyName(item.Key);

                        JsonSerializer.Serialize(jsonWriter, item.Value,
                            item.Value?.GetType() ?? typeof(object));
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            return context.Response.WriteAsync(
                Encoding.UTF8.GetString(memoryStream.ToArray()));
        }
    }
}