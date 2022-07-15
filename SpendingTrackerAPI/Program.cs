using Microsoft.EntityFrameworkCore;
using SpendingTrackerAPI.Database;
using SpendingTrackerAPI.Exceptions;
using SpendingTrackerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
DatabaseSeed.Seed(app);
app.MapControllers();
app.Run();