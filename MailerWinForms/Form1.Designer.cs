using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using static System.Windows.Forms.LinkLabel;
using Timer = System.Windows.Forms.Timer;

namespace MailerWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            SuspendLayout();

            CreateMenuButtons();
            CreateSendMessageMenu();

            // 
            // Form1
            // 
            AutoScaleDimensions = new(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new(1920, 1080);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            Location = new(-9, 0);

            Name = "Mailer";
            Text = "Mailer";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        private void CreateMenuButtons()
        {
            smButton = new();
            smmButton = new();
            rmmButton = new();

            //
            // SendMessageMenuButton
            //
            smButton.Size = _commonFieldSize;
            smButton.Text = "Написать";
            smButton.Location = new(100, 100);
            smButton.TabIndex = 0;
            smButton.Click += GoToSendMessageMenuClick;
            //
            // SendedMenuButton
            //
            smmButton.Size = _commonFieldSize;
            smmButton.Text = "Отправленные";
            smmButton.Location = new(100, smButton.Bottom);
            smmButton.TabIndex = 0;
            smmButton.Click += GoToSendedMessagesMenu;
            //
            // RecievedMenuButton
            //
            rmmButton.Size = _commonFieldSize;
            rmmButton.Text = "Полученные";
            rmmButton.Location = new(100, smmButton.Bottom);
            rmmButton.TabIndex = 0;
            rmmButton.Click += GoToReceivedMessagesMenu;


            Controls.Add(smButton);
            Controls.Add(smmButton);
            Controls.Add(rmmButton);
        }


        private void GoToSendMessageMenuClick(object sender, EventArgs e)
        {
            Controls.Clear();
            InitializeComponent();
        }
        private void GoToSendedMessagesMenu(object sender, EventArgs e)
        {
            Controls.Clear();
            CreateMenuButtons();
            CreateSendedMessagesMenu();
        }
        private void GoToReceivedMessagesMenu(object sender, EventArgs e)
        {
            Controls.Clear();
            CreateMenuButtons();
            CreateReceivedMessagesMenu();
        }


        private void CreateSendMessageMenu()
        {
            sText = new();
            sBox = new();
            rText = new();
            rBox = new();
            lText = new();
            cText = new();
            sButton = new();

            // 
            // senderTextBox
            // 
            sText.Location = new(500, 275);
            sText.Text = "Выберите Отправителя:";
            sText.Size = _commonFieldSize;
            sText.ReadOnly = true;
            sText.Enabled = false;
            sText.BorderStyle = BorderStyle.None;
            sText.TabIndex = 0;
            // 
            // senderComboBox
            // 
            sBox.FormattingEnabled = true;
            sBox.Location = new(sText.Left, sText.Bottom);
            sBox.Size = _commonFieldSize;
            sBox.Sorted = true;
            sBox.TabIndex = 0;
            // 
            // receiverTextBox
            // 
            rText.Location = new(1220, 275);
            rText.Text = "Выберите Получателя:";
            rText.Size = _commonFieldSize;
            rText.ReadOnly = true;
            rText.Enabled = false;
            rText.BorderStyle = BorderStyle.None;
            rText.TabIndex = 0;
            // 
            // receiverComboBox
            // 
            rBox.FormattingEnabled = true;
            rBox.Location = new(rText.Left, rText.Bottom);
            rBox.Size = _commonFieldSize;
            rBox.Sorted = true;
            rBox.TabIndex = 0;
            // 
            // letterNameTextBox
            // 
            lText.Location = new(660, 350);
            lText.PlaceholderText = "Введите название письма";
            lText.Size = new(_commonFieldSize.Width * 3, _commonFieldSize.Height);
            lText.TabIndex = 0;
            // 
            // letterContentTextBox
            // 
            cText.Location = new(lText.Left, lText.Bottom);
            cText.PlaceholderText = "Введите содержание письма";
            cText.AutoSize = false;
            cText.Size = new(_commonFieldSize.Width * 3, _commonFieldSize.Height * 15);
            cText.Multiline = true;
            cText.TabIndex = 0;
            //
            // suggestButton
            //
            sButton.Size = new(100, 25);
            sButton.Text = "Отправить";
            sButton.Location = new(cText.Right - sButton.Width, cText.Bottom);
            sButton.TabIndex = 0;
            sButton.Click += SendMessageClick;


            Controls.Add(sBox);
            Controls.Add(rBox);
            Controls.Add(sText);
            Controls.Add(rText);
            Controls.Add(lText);
            Controls.Add(cText);
            Controls.Add(sButton);

            foreach (var user in _users)
            {
                sBox.Items.Add(user.Name);
                rBox.Items.Add(user.Name);
            }
        }
        private void SendMessageClick(object sender, EventArgs e)
        {
            try
            {
                if (lText.Text == string.Empty || cText.Text == String.Empty)
                {
                    throw new Exception();
                }

                var requestedData = new LetterRequestModel()
                {
                    Name = lText.Text,
                    SenderId = _users.Single(x => x.Name == sBox.Text).Id,
                    ReceiverId = _users.Single(x => x.Name == rBox.Text).Id,
                    Content = cText.Text
                };

                string jsonData = JsonConvert.SerializeObject(requestedData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                _httpClient.PostAsync("/send", content);
            }
            catch
            {
                ThrowWarningWindow();
            }
        }


        private void CreateSendedMessagesMenu()
        {
            gButton = new();
            sText = new();
            sBox = new();

            // 
            // senderTextBox
            // 
            sText.Location = new(810, 75);
            sText.Text = "Выберите Отправителя:";
            sText.Size = _commonFieldSize;
            sText.ReadOnly = true;
            sText.Enabled = false;
            sText.BorderStyle = BorderStyle.None;
            sText.TabIndex = 0;
            // 
            // senderComboBox
            // 
            sBox.FormattingEnabled = true;
            sBox.Location = new(sText.Left, sText.Bottom);
            sBox.Size = _commonFieldSize;
            sBox.Sorted = true;
            sBox.TabIndex = 0;
            //
            // GetMessagesButton
            //
            gButton.Location = new(sBox.Right, sBox.Top);
            gButton.Size = new(100, sBox.Height);
            gButton.Text = "Показать";
            gButton.Name = "Sended";
            gButton.Click += GetMessages;


            Controls.Add(sText);
            Controls.Add(sBox);
            Controls.Add(gButton);

            foreach (var user in _users)
            {
                sBox.Items.Add(user.Name);
            }
        }
        private void GetMessages(object sender, EventArgs e)
        {
            try
            {
                List<Panel> outdatedControlsToRemove = this.Controls.OfType<Panel>().ToList();
                foreach (var control in outdatedControlsToRemove)
                {
                    Controls.Remove(control);
                }

                HttpResponseMessage response;

                

                if (((Button)sender).Name is "Sended")
                {
                    if (!_users.Contains(_users.Find(x => x.Name == sBox.Text)))
                    {
                        throw new Exception();
                    }
                    int id = _users.Find(x => x.Name == sBox.Text).Id;
                    response = _httpClient.GetAsync($"/s{id}").Result;
                }
                else
                {
                    if (!_users.Contains(_users.Find(x => x.Name == rBox.Text)))
                    {
                        throw new Exception();
                    }
                    int id = _users.Find(x => x.Name == rBox.Text).Id;
                    response = _httpClient.GetAsync($"/r{id}").Result;
                }

                string data = response.Content.ReadAsStringAsync().Result;
                _letters = JsonConvert.DeserializeObject<List<LetterModel>>(data);


                Panel panel = new();
                panel.Size = new(850, _letters.Count * 100);
                panel.Dock = DockStyle.Fill;
                panel.AutoScroll = true;

                Point position = new Point(560, 175);

                if (((Button)sender).Name is "Sended")
                {
                    foreach (var letter in _letters)
                    {
                        panel.Controls.Add(LetterTemplate(letter, position, true));
                        position.Y += 75;
                    }
                }
                else
                {
                    foreach (var letter in _letters)
                    {
                        panel.Controls.Add(LetterTemplate(letter, position, false));
                        position.Y += 100;
                    }
                }

                Controls.Add(panel);
            }
            catch
            {
                ThrowWarningWindow();
            }
            
        }


        private void CreateReceivedMessagesMenu()
        {
            gButton = new();
            rText = new();
            rBox = new();

            // 
            // receiverTextBox
            // 
            rText.Location = new(810, 75);
            rText.Text = "Выберите Получателя:";
            rText.Size = _commonFieldSize;
            rText.ReadOnly = true;
            rText.Enabled = false;
            rText.BorderStyle = BorderStyle.None;
            rText.TabIndex = 0;
            // 
            // receiverComboBox
            // 
            rBox.FormattingEnabled = true;
            rBox.Location = new(rText.Left, rText.Bottom);
            rBox.Size = _commonFieldSize;
            rBox.Sorted = true;
            rBox.TabIndex = 0;
            //
            // getMessagesButton
            //
            gButton.Location = new(rBox.Right, rBox.Top);
            gButton.Size = new(100, sBox.Height);
            gButton.Text = "Показать";
            gButton.Name = "Received";
            gButton.Click += GetMessages;


            Controls.Add(rText);
            Controls.Add(rBox);
            Controls.Add(gButton);

            foreach (var user in _users)
            {
                rBox.Items.Add(user.Name);
            }
        }

        private UserControl LetterTemplate(LetterModel model, Point location, bool isSender)
        {
            string mail;

            if (isSender)
            {
                 mail = _users.Find(x => x.Id == model.Receiver.Id).Mail;
            }
            else
            {
                 mail = _users.Find(x => x.Id == model.Sender.Id).Mail;
            }
            

            LetterTemplate letter = new(model.Name,model.Date, mail);
            letter.Location = location;
            letter.Into.Name = $"{model.Id}";
            letter.Into.Click += GetMessageById;

            return letter;
        }

        private void GetMessageById(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int id = int.Parse(b.Name);
            LetterModel letter = _letters.Find(x => x.Id == id);

            _letterForm.Size = new(600, 800);
            _letterForm.StartPosition = FormStartPosition.CenterScreen;
            _letterForm.FormBorderStyle = FormBorderStyle.None;
            _letterForm.BackColor = Color.Beige;

            sText = new();
            rText = new();
            dText = new();
            lText = new();
            cText = new();

            
            sText.Location = new(0, 0);
            sText.Text = $"Отправитель: {letter.Sender.Name} {letter.Sender.Mail}";
            sText.Font = new Font("Arial", 15);
            sText.Size = new(_letterForm.Width, 25);
            sText.ReadOnly = true;
            sText.Enabled = false;
            sText.BorderStyle = BorderStyle.None;
            sText.BackColor = _letterForm.BackColor;
            sText.TabIndex = 0;
            
            rText.Location = new(0, 50);
            rText.Text = $"Получатель: {letter.Receiver.Name} {letter.Receiver.Mail}";
            rText.Font = new Font("Arial", 15);
            rText.Size = new(_letterForm.Width, 25);
            rText.ReadOnly = true;
            rText.Enabled = false;
            rText.BorderStyle = BorderStyle.None;
            rText.BackColor = _letterForm.BackColor;
            rText.TabIndex = 0;

            dText.Location = new(0, 100);
            dText.Text = $"Отправлено: {letter.Date.ToShortDateString()} {letter.Date.ToShortTimeString()}";
            dText.Font = new Font("Arial", 15);
            dText.Size = new(_letterForm.Width, 25);
            dText.ReadOnly = true;
            dText.Enabled = false;
            dText.BorderStyle = BorderStyle.None;
            dText.BackColor = _letterForm.BackColor;
            dText.TabIndex = 0;

            lText.Location = new(0, 150);
            lText.Size = new(_letterForm.Width, 50);
            lText.Text = $"Тема: {letter.Name}";
            lText.Font = new Font("Arial", 15);
            lText.Multiline = true;
            lText.ReadOnly = true;
            lText.WordWrap = true;
            lText.BorderStyle = BorderStyle.None;
            lText.ScrollBars = ScrollBars.Vertical;
            lText.BackColor = _letterForm.BackColor;
            lText.TabIndex = 0;
            lText.TabStop = false;
            lText.Cursor = Cursors.Arrow;


            cText.Location = new(lText.Left, lText.Bottom+15);
            cText.Size = new(_letterForm.Width,525);
            cText.Text = letter.Content;
            cText.Font = new Font("Arial", 15);
            cText.Multiline = true;
            cText.ReadOnly = true;
            cText.BorderStyle = BorderStyle.None;
            cText.BackColor = _letterForm.BackColor;
            cText.ScrollBars = ScrollBars.Vertical;
            cText.TabIndex = 0;
            cText.TabStop = false;
            cText.Cursor = Cursors.Arrow;

            close = new();
            close.Text = "Ok";
            close.Size = new(50, 30);
            close.Location = new(275, 750);
            close.Click += CloseWarningClick;
            close.TabIndex = 0;


            _letterForm.Controls.Add(sText);
            _letterForm.Controls.Add(rText);
            _letterForm.Controls.Add(lText);
            _letterForm.Controls.Add(dText);
            _letterForm.Controls.Add(cText);
            _letterForm.Controls.Add(close);

            _letterForm.ShowDialog();
        }

        private void ThrowWarningWindow()
        {
            _additionalForm.Size = new(400, 100);
            _additionalForm.StartPosition = FormStartPosition.CenterScreen;
            _additionalForm.FormBorderStyle = FormBorderStyle.None;
            _additionalForm.BackColor = Color.Beige;

            //
            // WarningTextBox
            //
            TextBox text = new();
            text.Text = "Проверьте корректность введённых данных" +
                        "либо состояние апи сервера";
            text.Multiline = true;
            text.TextAlign = HorizontalAlignment.Center;
            text.Location = new(50, 10);
            text.Size = new(300, 50);
            text.BackColor = Color.Beige;
            text.ReadOnly = true;
            text.Enabled = false;
            text.BorderStyle = BorderStyle.None;
            //
            // CloseButton
            //
            close = new();
            close.Text = "Ok";
            close.Size = new(50, 30);
            close.Location = new(175, 60);
            close.Click += CloseWarningClick;


            _additionalForm.Controls.Add(text);
            _additionalForm.Controls.Add(close);

            _additionalForm.ShowDialog();
        }


        private void CloseWarningClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Parent.Hide();
            button.Parent.Controls.Clear();
        }

        #endregion

        private ComboBox sBox;
        private ComboBox rBox;
        private TextBox sText;
        private TextBox rText;
        private TextBox lText;
        private TextBox cText;
        private TextBox dText;
        private Button sButton;
        private Button smmButton;
        private Button rmmButton;
        private Button smButton;
        private Button gButton;
        private Button close;
    }
}