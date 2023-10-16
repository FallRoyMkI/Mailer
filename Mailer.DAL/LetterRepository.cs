using Mailer.Contracts;
using Mailer.Models;

namespace Mailer.DAL;

public class LetterRepository : ILetterRepository
{
    private readonly Context _context;

    public LetterRepository(Context context)
    {
        _context = context;
    }


    public bool IsLetterExist(int id)
    {
        return _context.Letters.Any(x => x.Id == id);
    }

    public void AddLetterToDb(Letter mail)
    {
        _context.Letters.Add(mail);
        _context.SaveChanges();
    }

    public Letter GetLetterById(int id)
    {
        return _context.Letters.Single(x => x.Id == id);
    }

    public List<Letter> GetAllLettersBySenderId(int id)
    {
        List<Letter> result = _context.Letters.Where(x => x.SenderId == id).ToList();
        return result;
    }

    public List<Letter> GetAllLettersByReceiverId(int id)
    {
        List<Letter> result = _context.Letters.Where(x => x.ReceiverId == id).ToList();
        return result;
    }
}