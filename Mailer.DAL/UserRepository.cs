using Faker;
using Mailer.Contracts;
using Mailer.Models;

namespace Mailer.DAL;

public class UserRepository : IUserRepository
{
    private readonly Context _context;

    public UserRepository(Context context)
    {
        _context = context;
    }


    public bool IsUserExist(int id)
    {
        return _context.Users.Any(x => x.Id == id);
    }

    public User GetUserById(int id)
    {
        return _context.Users.Single(x => x.Id == id);
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
}