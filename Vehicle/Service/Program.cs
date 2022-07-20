using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Service.AutoMapper;
using Service.DbContext;
using Service.DbContext.Seed;
using Service.Services.VehicleMake;
using Service.Services.VehicleModel;

Log.Logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .WriteTo.File($@"{Directory.GetCurrentDirectory()}\Logs\log.txt")
      .CreateLogger();


var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));
// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:DefaultConnection"]);
});
var mapper = config.CreateMapper();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVehicleMakeService, VehicleMakeService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddSingleton(mapper);

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

SeedData.EnsurePopulated(app);

app.Run();
