// Scaffold-DbContext "Server=localhost\sqlexpress; Database=College; User=sa; Password=1234567890; Encrypt=false" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

using Microsoft.EntityFrameworkCore;
using PRCT14_Server.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CollegeContext>(op => op.UseSqlServer(connection));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapControllers();

app.Run("http://0.0.0.0:5174");
