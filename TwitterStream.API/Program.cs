using Tweetinvi;
using TwitterStream.Repositories;
using TwitterStream.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITwitterClient, TwitterClient>(_ => new TwitterClient(
    builder.Configuration["Twitter:ApiKey"],
    builder.Configuration["Twitter:ApiKeySecret"],
    builder.Configuration["Twitter:BearerToken"])
);
builder.Services.AddSingleton<ITweetRepository, TweetRepository>();

builder.Services.AddHostedService<TweetStreamService>();

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