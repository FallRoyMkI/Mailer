namespace MailerWinForms;

public class LetterTemplate : UserControl
{

    public TextBox Name;
    public TextBox Mail;
    public TextBox Time;
    public Button Into;


    public LetterTemplate(string letterName, DateTime date, string mail)
    {
        this.Size = new Size(800, 50);
        this.BorderStyle = BorderStyle.FixedSingle;

        Into = new();
        Name = new();
        Mail = new();
        Time = new();

        Time.Location = new(0, 15);
        Time.Size = new(100, 30);
        Time.Text = date.ToShortDateString() + " " + date.ToShortTimeString();
        Time.Font = new Font("Arial", 10);
        Time.ReadOnly = true;
        Time.Enabled = false;
        Time.BorderStyle = BorderStyle.None;

        Name.Location = new(120, 15);
        Name.Size = new(400, 30);
        Name.Text = letterName;
        Name.Font = new Font("Arial", 8);
        Name.AutoSize = true;
        Name.ReadOnly = true;
        Name.Enabled = false;
        Name.BorderStyle = BorderStyle.None;

        Mail.Location = new(550, 15);
        Mail.Size = new(140, 30);
        Mail.Text = mail;
        Mail.Font = new Font("Arial", 8);
        Mail.ReadOnly = true;
        Mail.Enabled = false;
        Mail.BorderStyle = BorderStyle.None;

        Into.Location = new(690, 10);
        Into.Size = new(100, 30);
        Into.Text = "Подробнее";

        Controls.Add(Name);
        Controls.Add(Mail);
        Controls.Add(Time);
        Controls.Add(Into);
    }
}