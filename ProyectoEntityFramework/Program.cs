using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoEntityFramework;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>("Data Source=DESKTOP-JJF61NS\\SQLEXPRESS; Initial Catalog=TareasDB; Trusted_Connection=True; TrustServerCertificate=True");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());

});

app.MapGet("/api/tareas", async([FromServices] TareasContext dbContext) => 
    {
        return Results.Ok(dbContext.Tareas.Include(p=> p.Categoria).Where(p => p.PrioridadTarea == ProyectoEntityFramework.Models.Prioridad.Baja));
    });


app.Run();