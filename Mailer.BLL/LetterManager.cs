using System.Reflection;
using Mailer.Contracts;
using Mailer.Models;

namespace Mailer.BLL;

public class LetterManager : ILetterManager
{
    private readonly ILetterRepository _letters;
    private readonly IUserRepository _users;

    public LetterManager(ILetterRepository letterRepository, IUserRepository userRepository)
    {
        _letters = letterRepository;
        _users = userRepository;
    }


    public int SendLetter(string name, int senderId, int receiverId, string content)
    {
        if (!_users.IsUserExist(senderId) || !_users.IsUserExist(receiverId))
        {
            throw new Exception("User with such Id not exist");
        }

        Letter letter = new(name, senderId, receiverId, content);
        _letters.AddLetterToDb(letter);
        return letter.Id; 
    }

    public LetterResponse GetLetterById(int id)
    {
        if (!_letters.IsLetterExist(id))
        {
            throw new Exception("Letter with such Id not exist");
        }

        Letter letter = _letters.GetLetterById(id);

        User sender = _users.GetUserById(letter.SenderId);
        User receiver = _users.GetUserById(letter.ReceiverId);
        DateTime date = new(letter.Date.Year, letter.Date.Month, letter.Date.Day,
            letter.Date.Hour, letter.Date.Minute, 0);

        LetterResponse result = new(letter.Id, letter.Name, date, sender, receiver, letter.Content);

        return result;
    }

    public List<LetterResponse> GetAllLettersBySenderId(int id)
    {
        if (!_users.IsUserExist(id))
        {
            throw new Exception("User with such Id not exist");
        }

        List<Letter> letters = _letters.GetAllLettersBySenderId(id);
        List<LetterResponse> result = new();
        User sender = _users.GetUserById(id);

        foreach (var letter in letters)
        {
            User receiver = _users.GetUserById(letter.ReceiverId);
            DateTime date = new(letter.Date.Year, letter.Date.Month, letter.Date.Day,
                letter.Date.Hour, letter.Date.Minute, 0);

            LetterResponse response = new(letter.Id, letter.Name, date, sender, receiver, letter.Content);

            result.Add(response);
        }

        return result;
    }

    public List<LetterResponse> GetAllLettersByReceiverId(int id)
    {
        if (!_users.IsUserExist(id))
        {
            throw new Exception("User with such Id not exist");
        }

        List<Letter> letters = _letters.GetAllLettersByReceiverId(id);
        List<LetterResponse> result = new();
        User receiver = _users.GetUserById(id);

        foreach (var letter in letters)
        {
            User sender = _users.GetUserById(letter.SenderId);
            DateTime date = new(letter.Date.Year, letter.Date.Month, letter.Date.Day,
                letter.Date.Hour, letter.Date.Minute, 0);

            LetterResponse response = new(letter.Id, letter.Name, date, sender, receiver, letter.Content);

            result.Add(response);
        }

        return result;
    }

    public List<User> GetAllUsers()
    {
        return _users.GetAllUsers();
    }
}