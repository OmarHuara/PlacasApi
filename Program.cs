using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.NetCoreAll;
using PlacasAPI.Infrastructure.Persistence.DbContextConfig;
using PlacasAPI.Interfaces;
using PlacasAPI.Mappings;
using PlacasAPI.Rest;
using PlacasAPI.Services;
using PlacasAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DbConfigurationService.ConfigureServices(builder.Services, configuration);

builder.Services.AddElasticApm(new HttpDiagnosticsSubscriber());

builder.Services.AddScoped<HttpClient, HttpClient>();
builder.Services.AddScoped<IHtmlScrapingService, HtmlScrapingService>();
builder.Services.AddScoped<IAutomovelService, AutomovelService>();
builder.Services.AddSingleton(typeof(HtmlParserService));
builder.Services.AddAutoMapper(typeof(AutomovelMapping));

var app = builder.Build();
app.UseAllElasticApm(configuration);
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
