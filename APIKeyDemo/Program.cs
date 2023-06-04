using APIKeyDemo;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(/*x=> x.Filters.Add<ApiKeyAuthFilter>()*/);
//builder.Services.AddScoped<ApiKeyAuthMiddleware>();

builder.Services.AddScoped<ApiKeyAuthFilter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name="x-api-key",
        In=ParameterLocation.Header,
        Scheme="ApiKeyScheme",
        Description="Api key to access an endpoint"
    });
    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header,
    };

    var requirement = new OpenApiSecurityRequirement
    {
        {scheme,new List<string>() }
    };

    c.AddSecurityRequirement(requirement);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.MapGet("weathermini", () =>
{
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        //Summary = WeatherData.Summaries[Random.Shared.Next(WeatherData.Summaries.Length)]
    })
            .ToArray();
}).AddEndpointFilter<ApiKeyEndpointFilter>();

app.MapGet("weathermini2", () =>
{
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        //Summary = WeatherData.Summaries[Random.Shared.Next(WeatherData.Summaries.Length)]
    })
            .ToArray();
}).AddEndpointFilter<ApiKeyEndpointFilterCustomObjectResult>();

app.Run();
