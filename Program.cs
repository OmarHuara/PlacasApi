using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.NetCoreAll;
using Microsoft.Extensions.DependencyInjection;
using PlacasAPI.DataProviders;
using PlacasAPI.Infrastructure.Persistence.DbContextConfig;
using PlacasAPI.Infrastructure.Persistence.Repositories;
using PlacasAPI.Integration;
using PlacasAPI.Integration.ConsultaPlaca;
using PlacasAPI.Integration.KePlaca;
using PlacasAPI.Interfaces.Respositories;
using PlacasAPI.Interfaces.Services;
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
builder.Services.AddScoped<IHtmlScrapingServiceKePlaca, HtmlScrapingServiceKePlaca>();
builder.Services.AddScoped<IAutomovelService, AutomovelService>();
builder.Services.AddScoped<IAutomovelRepository, AutomovelRepository>();
//builder.Services.AddScoped<IPlateConsultIntegration, KePlacaIntegration>();

builder.Services.AddSingleton<ConsultaPlacaConfiguration>(s =>
    new ConsultaPlacaConfiguration("3XztkZuHDJuTPxYYc1CUVfTV4fzp6a5p"));

builder.Services.AddHttpClient(HttpClientDefaults.UrlConsultaPlacaComBR, (services, httpClient) =>
{
    httpClient.BaseAddress = new Uri("https://consultaplaca.com.br");
    httpClient.DefaultRequestHeaders.Add("cookie", $"PHPSESSID=ceopkkdagjmekoa3f1i60vlf4t; vaus=2; apiCt={services.GetRequiredService<ConsultaPlacaConfiguration>().Token}");
});

builder.Services.AddHttpClient(Microsoft.Extensions.Options.Options.DefaultName, httpClient =>
{
    httpClient.BaseAddress = new Uri("http://www.google.com");
});

builder.Services.AddScoped<IPlateConsultIntegration, ConsultaPlacaIntegration>();
builder.Services.AddScoped<IHtmlScrapingServiceConsultaPlaca, HtmlScrapingServiceConsultaPlaca>();

builder.Services.AddScoped<HtmlScrapingServiceConsultaPlaca>(s =>
    new HtmlScrapingServiceConsultaPlaca(
        s.GetRequiredService<IHttpClientFactory>(),
        s.GetRequiredService<ConsultaPlacaConfiguration>()
    ));


builder.Services.AddScoped<ConsultaPlacaIntegration>(s =>
    new ConsultaPlacaIntegration(
        s.GetRequiredService<IHtmlScrapingServiceConsultaPlaca>()
    ));

builder.Services.AddSingleton(typeof(HtmlParserService));
builder.Services.AddSingleton(typeof(GetRandomPlate));
builder.Services.AddAutoMapper(typeof(AutomovelMappingRG));

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
