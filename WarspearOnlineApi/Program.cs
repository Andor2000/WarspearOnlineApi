using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WarspearOnlineApi.Data;
using WarspearOnlineApi.Models;
using WarspearOnlineApi.Services.Journals;
using WarspearOnlineApi.Controllers;
using WarspearOnlineApi.Services;
using WarspearOnlineApi.Mapper;


// �������� ��������: add-migration InitMigration
var builder = WebApplication.CreateBuilder(args);

// ��������� ��������
ConfigureServices(builder);

// ����������� AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
// ��������� ������������� ���� ������
builder.Services.AddTransient<DatabaseInitializer>();

// ��������� ����������
var app = builder.Build();
ConfigureApplication(app, builder.Environment);

app.Run();

// ����� ��� ��������� ��������
static void ConfigureServices(WebApplicationBuilder builder)
{
    AddJwtAuthentication(builder.Services, builder.Configuration);

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddSingleton<JwtTokenService>();
    builder.Services.AddSingleton<AuthController>();

    builder.Services.AddScoped<JournalDropService>();
    builder.Services.AddScoped<JournalPlayerService>();

    builder.Services.AddScoped<DropService>();
    builder.Services.AddScoped<DropPlayerService>();
    builder.Services.AddScoped<PlayerService>();

    builder.Services.AddScoped<GenericService>();

    builder.Services.AddAuthorization();
    builder.Services.AddControllers();
}

// ����� ��� ��������� ���������� (middleware � ��������)
void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
{
    // ��������� middleware
    UseMiddlewareConfiguration(app, env);

    // ���������� �������� ��� ������� ����������
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate(); // ��������� ��� ��������, ������� ��� �� ���� ���������

        // ������������� ������ �������
        var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
        initializer.AddEmptyRecords(); // ������� ������ �������
    }

    // ��������� ���������
    app.UseRouting();
    app.MapControllers();
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

    app.UseAuthentication();
    app.UseAuthorization();
}
