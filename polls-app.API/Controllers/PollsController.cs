using Microsoft.AspNetCore.Mvc;
using polls_app.API.Models;
using polls_app.API.Services;

namespace polls_app.API.Controllers;

[Route("api/v1/polls")]
[ApiController]
public class PollsController : ControllerBase
{
    private readonly IPollService _pollService;

    public PollsController(IPollService pollService)
    {
        _pollService = pollService;
    }

    [HttpGet]
    public IActionResult GetAllPolls()
    {
        var polls = _pollService.GetAllPolls();
        return Ok(polls);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var poll = _pollService.GetPollById(id);
            return Ok(poll);
        }
        catch (Exception e)
        {
            return NotFound("Poll not found");
        }
        
    }

    [HttpPost]
    public IActionResult CreatePoll([FromBody] Poll poll) //crear dto
    {
        try
        {
            var createdPoll = _pollService.CreatePoll(poll);
            return CreatedAtAction(nameof(GetAllPolls), new {id = createdPoll.Id}, createdPoll);
        }
        catch (Exception e)
        {
            return BadRequest(new { message =  "Unable to create the poll." });
        }
        
    }

    [HttpPost("{id}/votes")]
    public IActionResult Vote(int id, [FromBody] VoteRequest voteRequest)
    {
        try
        {
            _pollService.Vote(id, voteRequest);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest("Unable to submit the vote.");
        }
        
    }
}