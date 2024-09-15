using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using GraphQL;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);

// Добавление служб для контроллеров и кэширования
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// Настройка подключения к базе данных SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация GraphQL
builder.Services.AddSingleton<RootQuery>();
builder.Services.AddSingleton<Schema>();

// Регистрация GraphQL сервера

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Конфигурация маршрутов
app.MapControllers();
app.UseGraphQL<Schema>(); // Устанавливаем маршрут для GraphQL
app.UseGraphQLPlayground(); // Устанавливаем маршрут для GraphQL Playground

app.Run();
