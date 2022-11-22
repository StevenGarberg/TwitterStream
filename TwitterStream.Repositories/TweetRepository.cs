using Tweetinvi.Models.V2;

namespace TwitterStream.Repositories;

public interface ITweetRepository
{
    Dictionary<string, TweetV2> Tweets { get; set; }
    void Insert(TweetV2 tweet);
    TweetV2[] Get();
    int Count();
    string[] GetTopHashtags(int count = 10);
}

public class TweetRepository : ITweetRepository
{
    public Dictionary<string, TweetV2> Tweets { get; set; } = new();
    private readonly Dictionary<string, int> HashtagCounts = new();

    public void Insert(TweetV2 tweet)
    {
        if (Tweets.ContainsKey(tweet.Id)) return;
            
        Tweets.Add(tweet.Id, tweet);

        if (tweet.Entities?.Hashtags == null || tweet.Entities.Hashtags.Length == 0) return;
        
        foreach (var hashtag in tweet.Entities.Hashtags)
        {
            if (HashtagCounts.ContainsKey(hashtag.Tag))
            {
                HashtagCounts[hashtag.Tag]++;
            }
            else
            {
                HashtagCounts.Add(hashtag.Tag, 1);
            }
        }
    }

    public TweetV2[] Get()
    {
        return Tweets.Values.ToArray();
    }

    public int Count()
    {
        return Tweets.Count;
    }

    public string[] GetTopHashtags(int count = 10)
    {
        if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count));
        
        return HashtagCounts
            .OrderByDescending(x => x.Value)
            .ThenBy(x => x.Key)
            .Select(x => x.Key)
            .Take(count)
            .ToArray();
    }
}