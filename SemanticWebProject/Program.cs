using Microsoft.EntityFrameworkCore;
using SemanticWebProject.BusinessLogic;
using SemanticWebProject.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configuration = builder.Configuration;
builder.Services.AddDbContext<EFContext>(options => options.UseNpgsql(
    configuration.GetConnectionString("SemanticConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<UniversityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder =>
    builder
        .AllowAnyHeader()
        .WithOrigins("https://s.travel.rv.ua")
        .AllowAnyMethod()
        .AllowCredentials());

app.MapControllers();

app.Run();