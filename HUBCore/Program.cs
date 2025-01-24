using IntelliAnalyticsCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using HUBCore.Hubs;
using HUBCore.Tools;

namespace IntelliAnalyticsCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar servicios
            builder.Services.AddControllers();

            // Limite para subir archivos grandes (100 MB)
            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });

            var misReglasCors = "ReglasCors";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: misReglasCors,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials(); // Para permitir el uso de credenciales si es necesario
                                  });
            });


            // Configurar Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configurar la cadena de conexi�n a la base de datos
            ConnDB.CadenaConexion = builder.Configuration.GetConnectionString("Conexion");

            builder.Services.AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10 MB
            });

            // Configurar SignalR
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configurar middlewares
            app.UseSwagger();
            app.UseSwaggerUI();

            // Usar CORS
            app.UseCors(misReglasCors);

            // Omitir HTTPS en el entorno
            // No redirigir HTTP a HTTPS
            app.Use((context, next) =>
            {
                context.Request.Scheme = "http";
                return next();
            });

            // Usar autorizaci�n (si la tienes configurada)
            app.UseAuthorization();

            // Mapear los controladores
            app.MapControllers();

            // Mapear SignalR hubs
            app.MapHub<TestHub>("/testHub");

            app.Run();
        }
    }
}
