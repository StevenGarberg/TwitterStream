using Microsoft.AspNetCore.Mvc;
using TwitterStream.API.Repositories;

namespace TwitterStream.API.Controllers;

[ApiController]
[Route("tweets")]
public class TweetController : ControllerBase
{
    private readonly ITweetRepository _tweetRepository;
    
    public TweetController(ITweetRepository tweetRepository)
    {
        _tweetRepository = tweetRepository;
    }
    
    [HttpGet("count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public IActionResult GetCount()
    {
        return Ok(_tweetRepository.Count());
    }

    [HttpGet("hashtags")]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
    public IActionResult GetHashtags()
    {
        return Ok(_tweetRepository.GetTopHashtags());
    }
}