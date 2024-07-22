using Microsoft.EntityFrameworkCore;
using TaxTool.Model;
using TaxTool.Model.Repository;
using TaxTool.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaxContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TaxDatabase")),
    ServiceLifetime.Singleton);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<ITaxRecordRepository, TaxRecordRepository>();
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
builder.Services.AddScoped<ITaxService, TaxService>();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();
app.Run();