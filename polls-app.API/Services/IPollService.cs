using polls_app.API.Models;

namespace polls_app.API.Services;

public interface IPollService
{
    List<Poll> GetAllPolls();
    Poll GetPollById(int id);
    Poll CreatePoll(Poll poll);
    PollOption Vote(int pollId, VoteRequest voteRequest);
}