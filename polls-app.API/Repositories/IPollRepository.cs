using polls_app.API.Models;

namespace polls_app.API.Repositories;

public interface IPollRepository
{
    List<Poll> GetAllPolls();
    Poll GetPollById(int id);
    Poll CreatePoll(Poll poll);
    PollOption Vote(int pollId, int optionId, string voterEmail);
}