using APIAggregator.Interfaces;
using APIAggregator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register HTTP clients for each API
builder.Services.AddHttpClient<IOpenWeatherMap, OpenWeatherMapService>();
builder.Services.AddHttpClient<IGitHubApi, GitHubApiService>();
builder.Services.AddHttpClient<ICatFacts, CatFactsService>();

// Add Razor Pages support
builder.Services.AddRazorPages(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable serving static files
app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Map Razor Pages
app.MapRazorPages();

app.Run();
