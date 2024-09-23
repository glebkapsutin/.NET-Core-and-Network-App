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
builder.Services.AddScoped<RootQuery>();
builder.Services.AddScoped<ISchema, Schema>(provider =>
{
    var schema = new Schema(provider)
    {
        Query = provider.GetRequiredService<RootQuery>()
    };
    schema.Initialize();
    return schema;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Конфигурация маршрутов
app.MapControllers();
app.UseGraphQL<ISchema>(); // Устанавливаем маршрут для GraphQL
app.UseGraphQLPlayground(); // Устанавливаем маршрут для GraphQL Playground

app.Run();
