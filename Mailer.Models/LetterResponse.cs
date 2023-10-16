namespace Mailer.Models;

public class LetterResponse
{
    public int Id { get; set; }

    public string Name { get; set; }
    public DateTime Date { get; set; }
    public User Sender { get; set; }
    public User Receiver { get; set; }
    public string Content { get; set; }


    public LetterResponse(int id, string name, DateTime date, User sender, User receiver, string content)
    {
        Id = id;
        Name = name;
        Date = date;
        Sender = sender;
        Receiver = receiver;
        Content = content;
    }
}