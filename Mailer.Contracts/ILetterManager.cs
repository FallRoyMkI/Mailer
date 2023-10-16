using Mailer.Models;

namespace Mailer.Contracts;

public interface ILetterManager
{
    public int SendLetter(string name, int sender, int receiver, string content);
    public LetterResponse GetLetterById(int id);
    public List<LetterResponse> GetAllLettersBySenderId(int id);
    public List<LetterResponse> GetAllLettersByReceiverId(int id);
    public List<User> GetAllUsers();
}