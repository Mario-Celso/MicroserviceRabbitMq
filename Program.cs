
using MicroserviceRabbitMq.Configuration;
using MicroserviceRabbitMq.Services;

namespace MicroserviceRabbitMq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddSingleton<MessageService>();  // Registrar MessageService aqui
            // Add services to the container.
            ConfigureServices(builder.Services);


            // Carregar configurações do appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.WebHost.UseUrls("http://*:80");

            // Configurar classe RabbitMQSettings
            var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();
            if (rabbitMQSettings != null)
            {
                builder.Services.AddSingleton(rabbitMQSettings);
                Console.WriteLine($"RabbitMQSettings configurado com sucesso: HostName={rabbitMQSettings.HostName}, UserName={rabbitMQSettings.UserName}, Password={rabbitMQSettings.Password}");
            }
            else
            {
                throw new InvalidOperationException("RabbitMQSettings não configurado corretamente.");
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroserviceRabbitMq - api");
                });
            }

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddSingleton<RabbitMQClientService>();

        }
    }
}
