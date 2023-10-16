using System.ComponentModel.DataAnnotations;

namespace Mailer.Models;
public class Letter
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }


    public Letter(string name, int sender, int receiver, string content)
    {
        Name = name;
        Date = DateTime.Now;
        SenderId = sender;
        ReceiverId = receiver;
        Content = content;
    }

    public Letter() { }
}
