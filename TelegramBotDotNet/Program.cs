using Telegram.Bot;
using TelegramBotDotNet.Mappers;
using TelegramBotDotNet.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<HttpService>();
builder.Services.AddSingleton<Mapper>();
builder.Services.AddSingleton<ForecastUrlBuilder>();
builder.Services.AddSingleton<TelegramBotClient>(sp =>
    new TelegramBotClient("7378936300:AAGfIrH1JNC21GklSW_r-O5z4_hg2-WTzgk"));
builder.Services.AddSingleton<TelegramService>(sp =>
    new TelegramService("7378936300:AAGfIrH1JNC21GklSW_r-O5z4_hg2-WTzgk", 123456789));
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddSingleton<BotListener>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var botListener = app.Services.GetRequiredService<BotListener>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/fetch-data", async (HttpService httpService) =>
{
    var url = "https://open-meteo.com/en/docs#latitude=-41.2283&longitude=174.8702&current=&hourly=temperature_2m,relative_humidity_2m,precipitation,weather_code,wind_speed_10m,wind_direction_10m,uv_index&wind_speed_unit=ms&timezone=Pacific%2FAuckland&forecast_days=1";
    //string data = await httpService.FetchDataFromUrl(url);
   // return Results.Ok(data);
});

app.Run();