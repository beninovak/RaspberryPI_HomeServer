using HomeServer.Core.DTOModelConverters;
using HomeServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using HomeServer.Server.Data;
using HomeServer.Server.Repositories;
using HomeServer.Server.Services;

// TODO - Dej to u environment variable
const string DBSource = "Host=localhost;Port=5432;Database=homeserver;Username=homeserver_user;Password=ko_te_jebe_333";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services
    .AddControllers()
    .AddJsonOptions(options => {
        // Need this, otherwise my PascalCase properties are sent to the client as camelCase.
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MainDbContext>(options => {
    options.UseNpgsql(DBSource)
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging() // TODO - Preber si o tem...kje se loggajo to scene?
        .EnableDetailedErrors();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

builder.Services.AddScoped<IUserConverter, UserConverter>();
builder.Services.AddScoped<ITodoConverter, TodoConverter>();
builder.Services.AddScoped<IExpenseConverter, ExpenseConverter>();

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();