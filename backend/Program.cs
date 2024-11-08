using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using backend.Services;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PolygonContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;  // Увеличим максимальную глубину (по умолчанию 32)
    });

builder.Services.AddScoped<PolygonService>();
builder.Services.AddScoped<PolygonDatabaseService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Polygon API",
        Version = "v1",
        Description = "API для работы с полигонами и точками"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PolygonContext>();
    try
    {
        dbContext.Database.Migrate();
        dbContext.Database.EnsureCreated();  
        Console.WriteLine("База данных успешно создана или обновлена.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Произошла ошибка при миграции базы данных: {ex.Message}");
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAllOrigins");
//app.UseHttpsRedirection();
app.MapControllers();
app.Run();
