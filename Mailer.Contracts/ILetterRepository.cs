using Mailer.Models;

namespace Mailer.Contracts;

public interface ILetterRepository
{
    public void AddLetterToDb(Letter mail);
    public bool IsLetterExist(int id);
    public Letter GetLetterById(int id);
    public List<Letter> GetAllLettersBySenderId(int id);
    public List<Letter> GetAllLettersByReceiverId(int id);
    
}