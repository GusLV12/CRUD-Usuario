using Microsoft.EntityFrameworkCore;
using CrudUserAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework Core con InMemory
builder.Services.AddDbContext<APIContext>(options =>
    options.UseInMemoryDatabase("UsuariosDb"));

// Configurar CORS para permitir cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// 📌 **Aplicar CORS antes de Authorization**
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
