using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlantandBiologyRecognition.BLL.Services;
using Microsoft.OpenApi.Models;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Repositories.Implements;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Services.Implements;

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
    builder.Services.AddScoped<IFeedbackService, FeedbackService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<ILearningTipService, LearningTipService>();
    builder.Services.AddScoped<IUserRoleService, UserRoleService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ITextbooklinkService, TextbooklinkService>();
    RegisterApplicationServices();
}


//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseCors("MyDefaultPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void RegisterApplicationServices()
{
    // Register your service so it can resolve dependencies
    builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
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

