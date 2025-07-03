using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Eliminar la instancia de la DB real
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TaskManagerDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            // Agregar DB InMemory para pruebas
            services.AddDbContext<TaskManagerDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Asegurar que se inicializa la base de datos
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
