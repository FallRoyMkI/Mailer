namespace MailerWinForms;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }

    public User(int id, string name, string mail)
    {
        Id = id;
        Name = name;
        Mail = mail;
    }
}