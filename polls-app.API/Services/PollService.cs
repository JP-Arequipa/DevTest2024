using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using polls_app.API.Models;
using polls_app.API.Repositories;

namespace polls_app.API.Services;

public class PollService : IPollService
{
    private readonly IPollRepository _pollRepository;

    public PollService(IPollRepository pollRepository)
    {
        _pollRepository = pollRepository;
    }


    public List<Poll> GetAllPolls()
    {
        return _pollRepository.GetAllPolls();
    }

    public Poll GetPollById(int id)
    {
        return _pollRepository.GetPollById(id);
    }

    public Poll CreatePoll(Poll poll)
    {
        if (string.IsNullOrWhiteSpace(poll.Name))
        {
            throw new Exception("Poll name must not be empty.");
        }

        if (Regex.IsMatch(poll.Name, @"[^a-zA-Z0-9\s]"))
        {
            throw new Exception("Poll name must only contain letters, numbers, and spaces.");
        }

        
        if (poll.Options.Count < 2)
        {
            throw new Exception("Poll must have at least two options.");
        }

        foreach (var option in poll.Options)
        {
            if (string.IsNullOrWhiteSpace(option.Name))
            {
                throw new Exception("Poll option names must not be empty.");
            }
        }
        
        return _pollRepository.CreatePoll(poll);
    }

    public PollOption Vote(int pollId, VoteRequest voteRequest)
    {
        var poll = GetPollById(pollId);
        
        if (poll == null)
        {
            throw new Exception("Poll must exist");
        }

        if (!IsValidEmail(voteRequest.VoterEmail))
        {
            throw new Exception("VoterEmail must be a valid email address.");
        }

        var option = poll.Options.FirstOrDefault(o => o.Id == voteRequest.OptionId);
        if (option == null)
        {
            throw new Exception("OptionId must exist. ");
        }
        
        return _pollRepository.Vote(pollId, voteRequest.OptionId, voteRequest.VoterEmail);
    }

    private bool IsValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}