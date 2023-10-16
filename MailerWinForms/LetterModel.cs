namespace MailerWinForms;

public class LetterModel
{
    public int Id { get; set; }

    public string Name { get; set; }
    public DateTime Date { get; set; }
    public User Sender { get; set; }
    public User Receiver { get; set; }
    public string Content { get; set; }
}