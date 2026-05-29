using Microsoft.EntityFrameworkCore;
using HomeServer.Data;
using HomeServer.Middleware;
using HomeServer.Repositories;
using HomeServer.Services;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

string? dbHostname = config["HOME_SERVER_DB_HOST"];
string? dbPort =     config["HOME_SERVER_DB_PORT"];
string? dbName =     config["HOME_SERVER_DB_NAME"];
string? dbUsername = config["HOME_SERVER_DB_USER"];
string? dbPassword = config["HOME_SERVER_DB_PASS"];

if (dbHostname is null || dbPort is null || dbName is null || dbUsername is null || dbPassword is null) {
    throw new Exception("One or more database environment variables not found!");
}

string DBSource = $"Host={dbHostname};Port={dbPort};Database={dbName};Username={dbUsername};Password={dbPassword}";

// Add services to the container.
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
        .EnableSensitiveDataLogging() // TODO - Preber si o tem...kje se loggajo te scene?
        .EnableDetailedErrors();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseMiddleware<SessionAuthenticationMiddleware>();
app.UseExceptionHandler("/exceptions");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles(); 
app.MapControllers();
app.Run();