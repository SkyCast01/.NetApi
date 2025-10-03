using NasaWeatherApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.NumberHandling = JsonNumberHandling.Strict;
    op.JsonSerializerOptions.PropertyNamingPolicy = null;
});// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddHttpClient<GeoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
