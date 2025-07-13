using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using PlantandBiologyRecognition.BLL.Services.Implements;
using PlantandBiologyRecognition.BLL.Services.Interfaces;
using PlantandBiologyRecognition.BLL.Utils;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Repositories.Implements;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using PlantandBiologyRecognition.DAL.MetaDatas;

var builder = WebApplication.CreateBuilder(args);

// Ensure services are registered before building the host
ConfigureServices();
ConfigureDatabase();
ConfigureSwagger();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

void ConfigureServices()
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(ApiResponseBuilder.BuildErrorResponse<object>(
                    null,
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    "Invalid or missing authentication token"
                ));

                return context.Response.WriteAsync(result);
            }
        };
    });

    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    RegisterApplicationServices();
}

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

void RegisterApplicationServices()
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();
    builder.Services.AddScoped<IFeedbackService, FeedbackService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<ILearningTipService, LearningTipService>();
    builder.Services.AddScoped<IUserRoleService, UserRoleService>();
    builder.Services.AddScoped<IUserService, UserService>();
    // builder.Services.AddScoped<ITextbooklinkService, TextbooklinkService>();
    builder.Services.AddScoped<ISampleService, SampleService>();
    builder.Services.AddScoped<ISampleDetailService, SampleDetailService>();
    builder.Services.AddScoped<ISampleImageService, SampleImageService>();
    builder.Services.AddScoped<ISavedSampleService, SavedSampleService>();
    // Register your service so it can resolve dependencies
    builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
    builder.Services.AddScoped<JwtUtil>();
    builder.Services.AddScoped<IRefreshTokensService, RefreshTokensService>();
    builder.Services.AddScoped<IAuthService, AuthService>();
}

void ConfigureSwagger()
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "Your API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
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

