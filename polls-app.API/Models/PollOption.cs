namespace polls_app.API.Models;

public class PollOption
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Votes { get; set; }
}