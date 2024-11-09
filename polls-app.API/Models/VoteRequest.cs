namespace polls_app.API.Models;

public class VoteRequest
{
    public int OptionId { get; set; }
    public string VoterEmail { get; set; }
}