namespace Mailer.Models;

public class LetterRequestModel
{
    public string Name { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }
}