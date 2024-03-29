using Microsoft.EntityFrameworkCore;
using PassiveIncomeTracker.DbModels;
using PassiveIncomeTracker.Interfaces;
using PassiveIncomeTracker.Middlewares;
using PassiveIncomeTracker.OutsideServices;
using PassiveIncomeTracker.Repositories;
using PassiveIncomeTracker.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbApi>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddTransient<IJwtRepository, JwtRepository>();
builder.Services.AddTransient<ICoinMarketCapService, CoinMarketCapService>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICryptocurrenciesService, CryptocurrenciesService>();

builder.Services.AddTransient<IUserInterestService, UserInterestService>();
builder.Services.AddTransient<ICodesService, CodesService>();
builder.Services.AddTransient<ICryptocurrenciesFavouritesService, CryptocurrenciesFavouritesService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
