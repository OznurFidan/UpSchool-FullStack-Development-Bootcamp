using Application;
using Application.Common.Interfaces;
using Domain.Dtos;
using Domain.Settings;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text;
using WebApi.Filters;
using WebApi.Hubs;
using WebApi.Services;

//var builder = WebApplication.CreateBuilder(args);
//builder.Host.UseSerilog();

//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//// Add services to the container.

//builder.Services.AddControllers(opt =>
//{
//    opt.Filters.Add<ValidationFilter>();

//});


//var config = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("C:\\Users\\fidan\\OneDrive\\Belgeler\\GitHub\\UpSchool-FullStack-Development-Bootcamp\\UpSchool-FullStack-Development-Bootcamp\\UpSchoolFinalProject\\WebApi\\appsettings.json") // Do�ru dosya yolunu buraya ekleyin
//    .Build();


//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

//builder.Services.Configure<GoogleSettings>(builder.Configuration.GetSection("GoogleSettings"));


//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(o =>
//    {
//        o.RequireHttpsMetadata = false;
//        o.SaveToken = false;

//    });




//builder.Services.AddSignalR();

//var mariaDbConnectionString = builder.Configuration.GetConnectionString("MariaDB")!;

//builder.Services.AddApplicationServices();
//builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.WebRootPath);

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapControllers();

//app.MapHub<SeleniumLogHub>("/Hubs/SeleniumLogHub");

//app.Run();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddScoped<ICurrentUserService, CurrentUserManager>();

    // Add services to the container.

    builder.Services.AddControllers(opt =>
    {
        //opt.Filters.Add<ValidationFilter>();
        opt.Filters.Add<GlobalExceptionFilter>();
    });

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

    builder.Services.Configure<GoogleSettings>(builder.Configuration.GetSection("GoogleSettings"));

    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(setupAction =>
    {
        setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = $"Input your Bearer token in this format - Bearer token to access this API",
        });
        setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            }, new List<string>()
        },
    });
    });

    // Add services to the container.
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.WebRootPath);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
            };

            o.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/Hubs/OrdersHub") || (path.StartsWithSegments("/Hubs/OrderHub"))))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });

    // Localization Files' Path
    //builder.Services.AddLocalization(options =>
    //{
    //    options.ResourcesPath = "Resources";
    //});

    //builder.Services.Configure<RequestLocalizationOptions>(options =>
    //{
    //    var defaultCulture = new CultureInfo("en-GB");

    //    List<CultureInfo> cultureInfos = new List<CultureInfo>()
    //{
    //    defaultCulture, // en-GB
    //    new ("tr-TR")
    //};

    //    options.SupportedCultures = cultureInfos;

    //    options.SupportedUICultures = cultureInfos;

    //    options.DefaultRequestCulture = new RequestCulture(defaultCulture);

    //    options.ApplyCurrentCultureToResponseHeaders = true;
    //});

    builder.Services.AddSignalR();

    //builder.Services.AddScoped<IAccountHubService, AccountHubManager>();

    builder.Services.AddMemoryCache();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed((host) => true)
                .AllowAnyHeader());
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();

    // Localization
    //var requestLocalizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
    //if (requestLocalizationOptions is not null) app.UseRequestLocalization(requestLocalizationOptions.Value);

    app.UseHttpsRedirection();

    app.UseCors("AllowAll");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.MapHub<OrdersHub>("/Hubs/OrdersHub");
    app.MapHub<SeleniumLogHub>("/Hubs/SeleniumLogHub");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}