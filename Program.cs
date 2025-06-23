using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Serilog;
using DotNetEnv;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
//ENV variables
Env.Load();

//FRONTEND 
var FrontendCors = "AllowFrontendCors";
builder.Services.Configure<FrontendSettings>(builder.Configuration.GetSection("FrontendSettings"));

//Stripe
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<IStripeUserAccountDetailsRepository, StripeUserAccountDetailsRepository>();

//ShoppingCart
builder.Services.AddScoped<ICheckoutService, CheckoutService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: FrontendCors,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5173", "https://b5e7-149-156-124-21.ngrok-free.app")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

//Product related
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Auth with jwt
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTService, JWTService>();

//User related
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Seacrh
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

//Security utils
builder.Services.AddScoped<ISecurityUtils, SecurityUtils>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

//DATABASE
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

//Mappers
builder.Services.AddAutoMapper(typeof(Program));

//JWT
var JWT_SECRET_KEY = builder.Configuration["Jwt:Secret"];
builder.Services.AddIdentityCore<User>(cfg =>
  {
    cfg.User.RequireUniqueEmail = true;
  })
.AddEntityFrameworkStores<DataContext>();


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "Allexgro.Backend",
            ValidAudience = "Allexgro.Frontend",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET_KEY))
        };
    });

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<JwtSettings>>().Value
);

//Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/app-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

//Context Accessor
builder.Services.AddHttpContextAccessor();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseSerilogRequestLogging();
app.UseCors(FrontendCors);
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
