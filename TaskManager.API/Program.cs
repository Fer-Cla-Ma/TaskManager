using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TaskManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//only configuration 
// Seeds the database with initial data for development and testing purposes.
//void EnsureDatabaseSeeded(IServiceProvider services)
//{
//    using var scope = services.CreateScope();
//    var dbContext = scope.ServiceProvider.GetRequiredService<TaskManagerDbContext>();

//    // Ensure database exists (development only)
//    dbContext.Database.EnsureCreated();

//    // Add a test item if none exist
//    if (!dbContext.TaskItems.Any())
//    {
//        var user = new User
//        {
//            UserName = "admin",
//            Email = "admin@example.com",
//            FullName = "Administrador",
//            PasswordHash = "hashed_password", // demo only
//            IsActive = true
//        };

//        var task = new TaskItem
//        {
//            Title = "Primera tarea",
//            Description = "Esta es una tarea de prueba",
//            IsCompleted = false,
//            User = user
//        };

//        dbContext.Users.Add(user);
//        dbContext.TaskItems.Add(task);
//        dbContext.SaveChanges();
//    }

//    // Read and print the data
//    var tasks = dbContext.TaskItems.ToList();
//    foreach (var task in tasks)
//    {
//        Console.WriteLine($"Tarea: {task.Title} - Completada: {task.IsCompleted}");
//    }
//}
