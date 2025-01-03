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


// �������� ��������: add-migration InitMigration
// ������
// dotnet ef migrations add InitMigration --project "D:\Project\WarspearOnlineApi\WarspearOnlineApi\WarspearOnlineApi.csproj"
// ���
// dotnet ef migrations add InitMigration --project "F:\WarspearOnlineApi\WarspearOnlineApi\WarspearOnlineApi.csproj"
// �����
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

// ��������� ��������
ConfigureServices(builder);

// ����������� AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// ��������� ������������� ���� ������
builder.Services.AddTransient<DatabaseInitializer>();

// ��������� ����������
var app = builder.Build();
app.UseCors("AllowAll");
ConfigureApplication(app, builder.Environment);
app.Run();

// ����� ��� ��������� ��������
static void ConfigureServices(WebApplicationBuilder builder)
{
    AddJwtAuthentication(builder.Services, builder.Configuration);

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    /* ������� ����������� �� �������� �������. ������ �� ����� ������ ������ �������� ����������� */
    var baseServiceType = typeof(BaseService);
    var assembly = Assembly.GetExecutingAssembly();

    var serviceTypes = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(baseServiceType));
    foreach (var serviceType in serviceTypes)
    {
        builder.Services.AddScoped(serviceType);
    }
    /* ����� */

    builder.Services.AddSingleton<AuthController>();
    builder.Services.AddSingleton<JwtTokenService>();

    builder.Services.AddAuthorization();
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();
}

///
void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
{
    // ��������� middleware
    UseMiddlewareConfiguration(app, env);

    // ���������� �������� ��� ������� ����������
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate(); // ��������� ��� ��������, ������� ��� �� ���� ���������
        dbContext.Database.ExecuteSqlRaw("ALTER DATABASE WarspearOnlineApi COLLATE Cyrillic_General_CI_AS;");

        // ������������� ������ �������
        var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
        initializer.AddEmptyRecords(); // ������� ������ �������
        initializer.AddBaseRecords();
    }
}

// ����� ��� ��������� �������������� ����� JWT
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

// ����� ��� ��������� middleware
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

    app.UseRouting(); // ������������� ������ ���� �� �����������

    // ��������������
    app.UseAuthentication();

    // �����������
    app.UseAuthorization();

    // ����������� �������� ����� (Endpoints)
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
