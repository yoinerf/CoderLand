using Microsoft.EntityFrameworkCore;
using AutoApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllers();

builder.Services.AddDbContext<AutoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MarcasAutosConnection")));

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
