namespace polls_app.API.Models;

public class Poll
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PollOption> Options { get; set; } = new List<PollOption>();
}