using Mailer.Models;
using Microsoft.EntityFrameworkCore;

namespace Mailer.DAL;

public class Context : DbContext
{
    public DbSet<Letter> Letters { get; set; }
    public DbSet<User> Users { get; set; }
    public Context(DbContextOptions<Context> options) : base(options) { }
}