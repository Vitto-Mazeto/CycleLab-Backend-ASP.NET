using ExercicioWebAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ExcWebAPIContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"),
        assembly => assembly.MigrationsAssembly(typeof(ExcWebAPIContext).Assembly.FullName));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
Console.WriteLine("Uau");

app.MapControllers();

app.Run();
