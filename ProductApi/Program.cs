using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Добавление служб для контроллеров и кэширования
builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // Добавляем кэширование в память
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Используем SQL Server

var app = builder.Build();

// Конфигурация промежуточного ПО
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Настройка маршрутизации для контроллеров
});

app.Run();
