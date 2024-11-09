using Microsoft.EntityFrameworkCore;
using polls_app.API.Data;
using polls_app.API.Models;

namespace polls_app.API.Repositories;

public class EntityFrameworkPollRepository : IPollRepository
{
    private readonly AppDbContext _context;

    public EntityFrameworkPollRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Poll> GetAllPolls()
    {
        return _context.Set<Poll>().Include(p => p.Options).ToList();
    }

    public Poll GetPollById(int id)
    {
        return _context.Set<Poll>().Include(p => p.Options).FirstOrDefault(p => p.Id == id);
    }

    public Poll CreatePoll(Poll poll)
    {
        _context.Set<Poll>().Add(poll);
        _context.SaveChanges();
        return poll;
    }

    public PollOption Vote(int pollId, int optionId, string voterEmail)
    {
        var poll = GetPollById(pollId);
        if (poll==null)
        {
            throw new Exception("Poll not found");
        }

        var option = poll.Options.FirstOrDefault(o => o.Id == optionId);
        if (option == null)
        {
            throw new Exception("Option not found");
        }

        option.Votes++;
        _context.SaveChanges();
        return option;
    }
}