using Mailer.Models;

namespace Mailer.Contracts;

public interface IUserRepository
{
    public bool IsUserExist(int id);
    public User GetUserById(int id);
    public List<User> GetAllUsers();
}