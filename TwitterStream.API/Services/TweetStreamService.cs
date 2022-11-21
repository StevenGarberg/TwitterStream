using Tweetinvi;
using TwitterStream.API.Repositories;

namespace TwitterStream.API.Services;

public class TweetStreamService : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ITweetRepository _tweetRepository;
    private readonly ITwitterClient _twitterClient;

    public TweetStreamService(
        IConfiguration configuration,
        ITweetRepository tweetRepository,
        ITwitterClient twitterClient)
    {
        _configuration = configuration;
        _tweetRepository = tweetRepository;
        _twitterClient = twitterClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var stream = _twitterClient.StreamsV2.CreateSampleStream();
        stream.TweetReceived += (_, eventArgs) =>
        {
            _tweetRepository.Insert(eventArgs.Tweet);
        };
        await stream.StartAsync();
    }
}