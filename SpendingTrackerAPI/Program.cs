using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpendingTrackerAPI;
using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Exceptions;
using SpendingTrackerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Auth
var authorizationSettings = new AuthSettings();
builder.Configuration.GetSection("Authentication").Bind(authorizationSettings);
builder.Services.AddSingleton(authorizationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authorizationSettings.JwtIssuer,
        ValidAudience = authorizationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authorizationSettings.JwtKey)),
    };
});

// Add database connection
var server = builder.Configuration["DbServer"] ?? "sqlserver";
var port = builder.Configuration["DbPort"] ?? "1433";
var user = builder.Configuration["DBUser"] ?? "SA";
var password = builder.Configuration["DBPassword"] ?? "Password_1234567";
var database = builder.Configuration["Database"] ?? "SpendingTracker";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer($"Server={server},{port};Initial catalog={database};User ID ={user};Password={password}")
);
// Add Services
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IIncomeCategoryService, IncomeCategoryService>();
// Add ExceptionMiddleware
builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
DatabaseSeed.Seed(app);
app.MapControllers();
app.Run();