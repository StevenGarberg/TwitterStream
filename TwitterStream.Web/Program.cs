using Tweetinvi;
using TwitterStream.Repositories;
using TwitterStream.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ITwitterClient, TwitterClient>(_ => new TwitterClient(
    builder.Configuration["Twitter:ApiKey"],
    builder.Configuration["Twitter:ApiKeySecret"],
    builder.Configuration["Twitter:BearerToken"])
);
builder.Services.AddSingleton<ITweetRepository, TweetRepository>();

builder.Services.AddHostedService<TweetStreamService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();