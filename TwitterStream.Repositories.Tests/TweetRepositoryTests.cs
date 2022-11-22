using FluentAssertions;
using Tweetinvi.Models.V2;

namespace TwitterStream.Repositories.Tests;

public class TweetRepositoryTests
{
    private readonly TweetRepository _tweetRepository;

    public TweetRepositoryTests()
    {
        _tweetRepository = new()
        {
            Tweets = new Dictionary<string, TweetV2>
            {
                { "foo", new TweetV2 { Entities = new TweetEntitiesV2 { Hashtags = new[] { new HashtagV2 { Tag = "kez" } } } } },
                { "bar", new TweetV2() }
            }
        };
    }
    
    [Fact]
    public void Count_Returns_Int()
    {
        _tweetRepository.Count().Should().Be(2);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(5)]
    [InlineData(20)]
    public void Foo_Returns_Requested_Number(int count)
    {
        _tweetRepository.GetTopHashtags().Length.Should().BeLessThanOrEqualTo(count);
    }
}