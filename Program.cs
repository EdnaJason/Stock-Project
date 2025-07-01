using FinShark.api.Data;
//using FinShark.api.Features.StockFeature.Query.GetAll;
using FinShark.api.Mappers;
using FinShark.api.Validators;
using FinShark.Domain.Repositories;
using FinShark.Service.Repositories;
using FinShark.Service.Stocks.Query.GetAll;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using FinShark.Service.Mappers;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(StockMappers));
builder.Services.AddAutoMapper(typeof(ServiceStockMappers));
builder.Services.AddMediatR(m => m.RegisterServicesFromAssemblies(typeof(GetAllStockQueryHandler).Assembly));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<GetByIdStockQueryValidator>();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);      // Register the ApplicationDBContext with the dependency injection container

builder.Services.AddScoped<IStockRepository, StockRepository>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();


builder.Host.UseSerilog();

var app = builder.Build();
// Configure the HTTP request pipeline. This is known as middlewares
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
