using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Tests.Integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        //Establecer el entorno como "Testing"
        builder.UseEnvironment("Testing");

        //Configurar servicios de pruebas
        builder.ConfigureServices(services =>
        {
            // Agregar DbContext con InMemoryDatabase (ya no es necesario quitar el anterior gracias a UseEnvironment)
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
            });

            // Asegurar que la base de datos se cree para los tests
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
