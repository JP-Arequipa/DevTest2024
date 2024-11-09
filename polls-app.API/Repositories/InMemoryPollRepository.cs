using polls_app.API.Models;

namespace polls_app.API.Repositories;

public class InMemoryPollRepository : IPollRepository
{
    private readonly List<Poll> _polls = new List<Poll>();
    private int _nextPollId = 1;
    private int _nextOptionId = 1;
    
    public List<Poll> GetAllPolls()
    {
        return _polls;
    }

    public Poll GetPollById(int id)
    {
        return _polls.FirstOrDefault(p => p.Id == id);
    }

    public Poll CreatePoll(Poll poll)
    {
        poll.Id = _nextPollId++;
        foreach (var option in poll.Options)
        {
            option.Id = _nextOptionId++;
        }
        _polls.Add(poll);
        _nextOptionId = 1;
        return poll;
    }

    public PollOption Vote(int pollId, int optionId, string voterEmail)
    {
        var poll = GetPollById(pollId);
        var option = poll.Options.FirstOrDefault(o => o.Id == optionId);
        option.Votes++;
        return option;
    }
}