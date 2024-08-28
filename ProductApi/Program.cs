using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ProductDb"));
var app = builder.Build();

// Конфигурация промежуточного ПО
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Настройка маршрутизации для контроллеров
});

app.Run();