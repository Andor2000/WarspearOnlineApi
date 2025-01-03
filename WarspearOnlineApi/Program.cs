using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WarspearOnlineApi.Api.Services.Users;
using WarspearOnlineApi.Api.Data;
using WarspearOnlineApi.Api.Models;
using WarspearOnlineApi.Api.Models.Mapper;
using WarspearOnlineApi.Api.Extensions;
using WarspearOnlineApi.Api.Controllers.Users;
using WarspearOnlineApi.Api.Services.Base;
using System.Reflection;


// Добавить миграцию: add-migration InitMigration
// работа
// dotnet ef migrations add InitMigration --project "D:\Project\WarspearOnlineApi\WarspearOnlineApi\WarspearOnlineApi.csproj"
// дом
// dotnet ef migrations add InitMigration --project "F:\WarspearOnlineApi\WarspearOnlineApi\WarspearOnlineApi.csproj"
// Арина
// dotnet ef migrations add InitMigration --project "C:\git-rep\WarspearOnlineApi\WarspearOnlineApi\WarspearOnlineApi.csproj"
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Настройка сервисов
ConfigureServices(builder);

// Регистрация AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Добавляем инициализатор базы данных
builder.Services.AddTransient<DatabaseInitializer>();

// Настройка приложения
var app = builder.Build();
app.UseCors("AllowAll");
ConfigureApplication(app, builder.Environment);
app.Run();

// Метод для настройки сервисов
static void ConfigureServices(WebApplicationBuilder builder)
{
    AddJwtAuthentication(builder.Services, builder.Configuration);

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    /* Сервисы наследуются от базового сервиса. Теперь не нужно каждый сервис отдельно прописывать */
    var baseServiceType = typeof(BaseService);
    var assembly = Assembly.GetExecutingAssembly();

    var serviceTypes = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(baseServiceType));
    foreach (var serviceType in serviceTypes)
    {
        builder.Services.AddScoped(serviceType);
    }
    /* Конец */

    builder.Services.AddSingleton<AuthController>();
    builder.Services.AddSingleton<JwtTokenService>();

    builder.Services.AddAuthorization();
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();
}

///
void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
{
    // Настройка middleware
    UseMiddlewareConfiguration(app, env);

    // Применение миграций при запуске приложения
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate(); // Применяет все миграции, которые еще не были применены
        dbContext.Database.ExecuteSqlRaw("ALTER DATABASE WarspearOnlineApi COLLATE Cyrillic_General_CI_AS;");

        // Инициализация пустых записей
        var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
        initializer.AddEmptyRecords(); // Вставка пустых записей
        initializer.AddBaseRecords();
    }
}

// Метод для настройки аутентификации через JWT
static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var jwtSettings = configuration.GetSection("JwtSettings");
    services.Configure<JwtSetting>(jwtSettings);

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
            };
        });
}

// Метод для настройки middleware
static void UseMiddlewareConfiguration(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseRouting(); // Маршрутизация должна быть до авторизации

    // Аутентификация
    app.UseAuthentication();

    // Авторизация
    app.UseAuthorization();

    // Определение конечных точек (Endpoints)
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
