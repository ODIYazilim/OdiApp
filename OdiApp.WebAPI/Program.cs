using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using OdiApp.BusinessLayer;
using OdiApp.BusinessLayer.Core;
using OdiApp.BusinessLayer.Core.Exceptions;
using OdiApp.BusinessLayer.Core.Filters;
using OdiApp.BusinessLayer.Core.Middlewares;
using OdiApp.BusinessLayer.Core.Services;
using OdiApp.DataAccessLayer;
using OdiApp.EntityLayer.Identity;
using OdiApp.WebAPI;
using Serilog;
using Serilog.Events;
using System.Globalization;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        tableName: "Logs",
        autoCreateSqlTable: true)
    .CreateLogger();

builder.Host.UseSerilog();
//builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
builder.Services.AddBusinessLayerServices();
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddDataLayerServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilterAttribute>();
});
SwaggerControllerOrder<ControllerBase> swaggerControllerOrder = new SwaggerControllerOrder<ControllerBase>(Assembly.GetEntryAssembly());
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("api", new OpenApiInfo()
    {
        Title = "ODIAPP API",
        Version = "v3"
    });

    options.OperationFilter<AddLanguageRequiredHeaderParameter>();

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

    options.OrderActionsBy((apiDesc) =>
        $"{swaggerControllerOrder.SortKey(apiDesc.ActionDescriptor.RouteValues["controller"])}");

    // XML dosyas� i�in g�venli kontrol
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
    else
    {
        // Opsiyonel: XML dosyas� bulunamazsa log olu�turun
        Console.WriteLine($"Warning: XML documentation file not found at {xmlPath}");
    }
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // �ifre gereksinimleri
    options.Password.RequireDigit = false;                   // Rakam gereklili�ini kald�r
    options.Password.RequiredLength = 1;                     // Minimum uzunlu�u 1'e d���r
    options.Password.RequireNonAlphanumeric = false;         // �zel karakter gereklili�ini kald�r
    options.Password.RequireUppercase = false;               // B�y�k harf gereklili�ini kald�r
    options.Password.RequireLowercase = false;               // K���k harf gereklili�ini kald�r

    // Kullan�c� gereksinimleri
    options.User.RequireUniqueEmail = false;                 // Benzersiz email gereklili�ini kald�r
    options.User.AllowedUserNameCharacters = null;           // Kullan�c� ad� karakter k�s�tlamas�n� kald�r

    // Kilit gereksinimleri
    options.Lockout.AllowedForNewUsers = false;             // Yeni kullan�c�lar i�in kilitlemeyi devre d��� b�rak
    options.Lockout.MaxFailedAccessAttempts = 1000;         // Maksimum ba�ar�s�z giri� denemesini art�r

    // Sign-in gereksinimleri
    options.SignIn.RequireConfirmedEmail = false;           // Email onay� gereklili�ini kald�r
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Telefon onay� gereklili�ini kald�r
    options.SignIn.RequireConfirmedAccount = false;         // Hesap onay� gereklili�ini kald�r
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokenOptions = TokenOptionService.GetTokenOption();
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("api/swagger.json", "OPIAPP Api D�k�man"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/api/swagger.json", "ODIAPP API v3");
    });
}
app.UseCors(options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().WithHeaders("Accept-Language"));
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<LocalizationHeaderException>();

app.UseHttpsRedirection();

var cultures = new List<CultureInfo> {
    new CultureInfo("en"),
    new CultureInfo("tr")
};
app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("tr-TR");
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();