using Microsoft.EntityFrameworkCore;
using HomeServer.Server.Data;

const string DBSource = "DataSource=/home/benino/Projects/RaspberryPI_HomeServer/DB.sqlite";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MainDbContext>(options => {
    options.UseSqlite(DBSource)
           .LogTo(Console.WriteLine)
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();