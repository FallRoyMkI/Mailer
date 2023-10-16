using Newtonsoft.Json;

namespace MailerWinForms
{
    public partial class Form1 : Form
    {
        private readonly Size _commonFieldSize = new(200, 25);
        private readonly HttpClient _httpClient = new();
        private List<User> _users;
        private List<LetterModel> _letters;
        private readonly Form _additionalForm = new();
        private readonly Form _letterForm = new();

        public Form1()
        {
            _letters = new();
            _users = new();

            try
            {
                _httpClient.BaseAddress = new Uri("https://localhost:7033/");
                HttpResponseMessage response = _httpClient.GetAsync("/u").Result;
                string data = response.Content.ReadAsStringAsync().Result;
                _users = JsonConvert.DeserializeObject<List<User>>(data);
                InitializeComponent();
            }
            catch
            {
                Text = "Ошибочка вышла";
                Size = new(500, 200);
                StartPosition = FormStartPosition.CenterScreen;
                TextBox message = new TextBox();
                message.Text = "Сервер в данный момент не доступен, перезапустите приложение с включённым апи";
                message.BorderStyle = BorderStyle.None;
                message.Size = new(400, 50);
                message.Multiline = true;
                message.TextAlign = HorizontalAlignment.Center;
                message.ReadOnly = true;
                message.Enabled = false;
                message.Location = new(50,25);
                Button b = new Button();
                b.Click += Close;
                b.Text = "Ok";
                b.Size = new(50, 25);
                b.Location = new(225, 75);

                Controls.Add(message);
                Controls.Add(b);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Close(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}