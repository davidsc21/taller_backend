using Api.Extensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.ConfigureCors();
builder.Services.AddCustomRateLimiter();
builder.Services.AddApplicationServices();;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("PostgresLocal")!;
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseCors("CorsPolicy");
app.UseCors("CorsPolicyUrl");
app.UseCors("Dinamica");

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

app.Run();