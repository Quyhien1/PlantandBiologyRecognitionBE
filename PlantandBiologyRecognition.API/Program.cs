using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlantandBiologyRecognition.BLL.Services.Implements;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Repositories.Implements;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Ensure services are registered before building the host
ConfigureServices();
ConfigureDatabase();
ConfigureSwagger();
var app = builder.Build();
void ConfigureServices()
{
    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();
    RegisterApplicationServices();
}


//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin =>
       origin.StartsWith("http://localhost:") ||
       origin.StartsWith("https://localhost:") ||
       origin.EndsWith(".vercel.app"))
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void RegisterApplicationServices()
{
    // Register your service so it can resolve dependencies
    builder.Services.AddScoped<IAccountService, AccountService>();
}

void ConfigureSwagger()
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "PlantandBiologyRecognition.API",
            Version = "v1",
            Description = "A PlantandBiologyRecognition System Project"
        });
    });
}
    void ConfigureDatabase()
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("SupaBaseConnection"), 
            npgsqlOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            });
    });
}

