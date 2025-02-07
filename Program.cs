using PlacasAPI.Interfaces;
using PlacasAPI.Mappings;
using PlacasAPI.Rest;
using PlacasAPI.Services;
using PlacasAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<HttpClient, HttpClient>();
builder.Services.AddScoped<IHtmlScrapingService, HtmlScrapingService>();
builder.Services.AddScoped<IAutomovelService, AutomovelService>();
builder.Services.AddSingleton(typeof(HtmlParserService));
builder.Services.AddAutoMapper(typeof(AutomovelMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
