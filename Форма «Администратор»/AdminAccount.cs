using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VisualArtsTrainingStudio.Форма__Ученик_;

namespace VisualArtsTrainingStudio
{
    public partial class AdminAccount : Form
    {
        public AdminAccount() => InitializeComponent();
        // Коллекция содержащая данные о записях
        private List<InformationTraining> Requests = new List<InformationTraining>();
        // Коллекция содержащая данные о преподавателях
        private List<Contacts> Teach = new List<Contacts>();
        // Обработка первоначальной загрузки формы
        private void AdminAccount_Load(object sender, EventArgs e)
        {
            // Очистка значений ComboBox и установка значений по умолчанию
            UpdateT = false;
            CB_Student.Items.Clear(); CB_StatusTraining.Items.Clear(); Requests.Clear(); Teach.Clear(); 
            CB_Student.Items.Add("Все"); CB_StatusTraining.Items.Add("В обработке"); CB_StatusTraining.Items.Add("Обучается"); CB_StatusTraining.Items.Add("Выпуск"); CB_StatusTraining.Items.Add("Отчислен");
            UpdateT =true;

            // Выполнение хранимых процедур и заполнение коллекции «Requests»
            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                // Получение данных согласно выполненной хранимой процедуры
                SQL_Connection.Open(); string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[InformationTraining_ALL]"; // SQL-запрос
                SqlCommand SQL_Command = new SqlCommand(Request, SQL_Connection); SqlDataReader Reader = SQL_Command.ExecuteReader();
                while (Reader.Read())
                {
                    int COST = 0; if (Reader.GetValue(4) != DBNull.Value) COST = (int)Reader.GetValue(3); Contacts Contact_Student = new Contacts("NULL", "", "", "", "", new DateTime()), Contact_Teach = new Contacts("NULL", "", "", "", "", new DateTime());
                    // Получение данных согласно выполненной хранимой процедуры
                    using (SqlConnection SQL_Connection2 = new SqlConnection(FormRequest.ConnectString))
                    {
                        SQL_Connection2.Open();
                        Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[Сontacts_Id] @Id"; // SQL-запрос
                        SqlCommand SQL_Command2 = new SqlCommand(Request, SQL_Connection2);
                        SQL_Command2.Parameters.Add("@Id", SqlDbType.VarChar, 7); SQL_Command2.Parameters["@Id"].Value = (string)Reader.GetValue(1); SqlDataReader Reader2 = SQL_Command2.ExecuteReader();
                        while (Reader2.Read()) Contact_Student = new Contacts((string)Reader2.GetValue(0), (string)Reader2.GetValue(1), (string)Reader2.GetValue(2), (string)Reader2.GetValue(5), (string)Reader2.GetValue(3), (DateTime)Reader2.GetValue(4));
                        bool T = true; foreach (string Student in CB_Student.Items) if (Student == $"{Contact_Student.LastName} {Contact_Student.FirstName[0]}. {Contact_Student.MiddleName[0]}.") T = false;
                        if (T) CB_Student.Items.Add($"{Contact_Student.LastName} {Contact_Student.FirstName[0]}. {Contact_Student.MiddleName[0]}."); SQL_Connection2.Close();
                    }
                    // Получение данных согласно выполненной хранимой процедуры
                    if (Reader.GetValue(6) != DBNull.Value) using (SqlConnection SQL_Connection2 = new SqlConnection(FormRequest.ConnectString))
                        {
                            SQL_Connection2.Open();
                            Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[Сontacts_Id] @Id"; // SQL-запрос
                            SqlCommand SQL_Command2 = new SqlCommand(Request, SQL_Connection2);
                            SQL_Command2.Parameters.Add("@Id", SqlDbType.VarChar, 7).Value = (string)Reader.GetValue(6); SqlDataReader Reader2 = SQL_Command2.ExecuteReader();
                            while (Reader2.Read())
                            {
                                Contact_Teach = new Contacts((string)Reader2.GetValue(0), (string)Reader2.GetValue(1), (string)Reader2.GetValue(2), (string)Reader2.GetValue(3), (string)Reader2.GetValue(4), (DateTime)Reader2.GetValue(5));
                                Contact_Teach.IDA((string)Reader.GetValue(7));
                            }
                            SQL_Connection2.Close();
                        }
                    // Добавление данных заявки в коллекцию «Requests»
                    Requests.Add(new InformationTraining((string)Reader.GetValue(0), (string)Reader.GetValue(2), COST, (string)Reader.GetValue(4), (string)Reader.GetValue(5), Contact_Student, Contact_Teach, (DateTime)Reader.GetValue(7)));
                }
                SQL_Connection.Close();
            }
            // Выполнение хранимой процедуры и заполнение коллекции «Teach»
            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open();
                string Request_SQL = $"EXEC [VisualArtsTrainingStudio].[dbo].[Teachs]"; // SQL-запрос
                SqlCommand SQL_Command = new SqlCommand(Request_SQL, SQL_Connection); SqlDataReader Reader = SQL_Command.ExecuteReader();
                while (Reader.Read())
                {
                    Contacts Log = new Contacts((string)Reader.GetValue(1), (string)Reader.GetValue(2), (string)Reader.GetValue(3), (string)Reader.GetValue(4), (string)Reader.GetValue(5), (DateTime)Reader.GetValue(6));
                    Log.IDA((string)Reader.GetValue(0)); Teach.Add(Log); // Добавление данных преподавателя в коллекцию «Teach» 
                }
                SQL_Connection.Close();
            }
            UpdateOut();
        }
        // Очистка интерфейса. Создание элементов управления согласно данным из коллекций и вывод их на интрефейсе
        private void UpdateOut()
        {
            Table.Controls.Clear(); UpdateT = false; 
            if (CB_StatusTraining.SelectedIndex == -1) CB_StatusTraining.SelectedIndex = 0; 
            if (CB_Student.SelectedIndex == -1) CB_Student.SelectedIndex = 0; UpdateT = true;
            // Обработка исключения «Данные отсутствуют»
            if (Requests.Count > 0)
                // Перебор коллекции «Requests» и вывод данных из неё согласно заданным условиям
                foreach (InformationTraining Request in Requests)
                    // Условие «Совпадение статуса работы заявки»
                    if (Request.StatusTraining == CB_StatusTraining.Items[CB_StatusTraining.SelectedIndex].ToString())
                        // Условие «Совпадение выбранного клиента» или «Вывод полного списка учеников»
                        if ((CB_Student.SelectedIndex == 0) | (CB_Student.Items[CB_Student.SelectedIndex].ToString() == $"{Request.Contact_Student.LastName} {Request.Contact_Student.FirstName[0]}. {Request.Contact_Student.MiddleName[0]}."))
                        {
                            // Создание элемента управления Panel содержащего данные заявки разделенные на подгруппы
                            Panel NewRequest = new Panel { Name = $"R_{Request.ID}", Size = new Size(Table.Width, 365), BorderStyle = BorderStyle.FixedSingle };
                            {
                                // Создание элемента управления Panel содержащего данные заявки для подгруппы #1
                                // Содержит данные заявки, такие как: номер, дата, статус работы, статус оплаты, стоимость
                                Panel Frame1 = new Panel { Name = "Frame1", Size = new Size(NewRequest.Width, 95), BorderStyle = BorderStyle.FixedSingle };
                                {
                                    // Создание элемента управления Label содержащего номер и дату заявки
                                    Label Number_Date = new Label { Text = $"Заявка №{Request.ID.Remove(0, 2)} от {Request.Date.Day}.{Request.Date.Month}.{Request.Date.Year}", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };
                                    // Создание элемента управления Label содержащего стоимость занятия
                                    Label Cost = new Label { Text = $"Стоимость: ", Font = new Font("Times New Roman", 16), Size = new Size(200, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 5, Top = 45 };
                                    // Вариант значения, если нет необходимости изменять стоимость
                                    if (Request.StatusTraining != "В обработке") { Cost.Name = "Cost"; Cost.Size = new Size(360, 40); Cost.Text += $"{Request.Cost} руб"; }
                                    // Вариант значения, если необходимо изменять стоимость
                                    else
                                    {
                                        // Создание элемента управления MaskedTextBox для ввода пользователем новой стоимости
                                        MaskedTextBox TB_Cost = new MaskedTextBox { Name = "Cost", Font = new Font("Times New Roman", 16), TextAlign = HorizontalAlignment.Center, Mask = "00 000 руб", Size = new Size(160, 40), Left = 205, Top = 50 };
                                        if (Request.Cost != 0) TB_Cost.Text = Request.Cost.ToString(); Frame1.Controls.Add(TB_Cost);
                                    }
                                    // Создание элемента управления Label содержащего статус обучения
                                    Label StatusTraining1 = new Label { Text = $"Статус обучения: ", Font = new Font("Times New Roman", 16), Size = new Size(160, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 375, Top = 45 };
                                    // Вариант, если нет необходимости изменять значение
                                    if (Request.StatusTraining == "Обучается" | Request.StatusTraining == "Отчислен")
                                    {
                                        Label StatusTraining2 = new Label { Name = "TrainingStatuses", Text = Request.StatusTraining, Font = new Font("Times New Roman", 16), Size = new Size(190, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 535, Top = 45 };
                                        if (Request.StatusTraining == "Обучается") StatusTraining2.ForeColor = Color.Green; else StatusTraining2.ForeColor = Color.Red; Frame1.Controls.Add(StatusTraining2);
                                    }
                                    // Вариант, если есть необходимость изменять значение
                                    else
                                    {
                                        ComboBox CB_StatusTraining1 = new ComboBox { Name = "TrainingStatuses", Font = new Font("Times New Roman", 16), Size = new Size(185, 40), Left = 535, Top = 50 };
                                        foreach (string Status in CB_StatusTraining.Items) CB_StatusTraining1.Items.Add(Status); CB_StatusTraining1.SelectedItem = Request.StatusTraining; Frame1.Controls.Add(CB_StatusTraining1);
                                    }
                                    // Создание элемента управления Label содержащего статус оплаты заявки
                                    Label StatusPayment1 = new Label { Text = $"Статус оплаты: ", Font = new Font("Times New Roman", 16), Size = new Size(160, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 730, Top = 45 };
                                    // Вариант, если нет необходимости изменять значение
                                    if (Request.StatusPayment == "Оплачено")
                                    {
                                        Label StatusPayment2 = new Label { Name = "PaymentStatuses", Text = Request.StatusPayment, Font = new Font("Times New Roman", 16), Size = new Size(140, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 890, Top = 45, ForeColor = Color.Green };
                                        Frame1.Controls.Add(StatusPayment2);
                                    }
                                    // Вариант, если есть необходимость изменять значение
                                    else
                                    {
                                        ComboBox CB_StatusPayment = new ComboBox { Name = "PaymentStatuses", Font = new Font("Times New Roman", 16), Size = new Size(140, 40), Left = 890, Top = 50 };
                                        CB_StatusPayment.Items.Add("Не Оплачено"); CB_StatusPayment.Items.Add("Оплачено"); CB_StatusPayment.SelectedItem = Request.StatusPayment; Frame1.Controls.Add(CB_StatusPayment);
                                    }
                                    Frame1.Controls.Add(Number_Date); Frame1.Controls.Add(Cost); Frame1.Controls.Add(StatusTraining1); Frame1.Controls.Add(StatusPayment1); NewRequest.Controls.Add(Frame1);
                                }
                                // Создание элемента управления Panel содержащего данные заявки для подгруппы #2
                                // Содержит данные заявки, такие как: направление
                                Panel Frame2 = new Panel { Size = new Size(NewRequest.Width - 18, 120), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height };
                                {
                                    // Создание элемента управления Label содержащего модель транспортного средства указанного в заявке
                                    Label Direction = new Label { Text = $"Направление: {Request.Direction}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width - 23, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10, BorderStyle = BorderStyle.FixedSingle };
                                    // Создание элемента управления PictureBox и Label, используемого как Button
                                    // Выполняет удаление выбранной заявки после повторного подтверждения
                                    PictureBox Pic_Delete = new PictureBox { BackgroundImage = Properties.Resources.Picture_Delete, BackgroundImageLayout = ImageLayout.Stretch, Size = new Size(40, 40), BorderStyle = BorderStyle.FixedSingle };
                                    Label Lab_Delete = new Label { Name = Request.ID, Text = "Удалить", Font = new Font("Times New Roman", 16), Size = new Size(100, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };
                                    Lab_Delete.Left = Frame2.Size.Width - Lab_Delete.Size.Width; Pic_Delete.Left = Lab_Delete.Left - Pic_Delete.Size.Width; Lab_Delete.Click += RequestDelete_Click;
                                    // Создание элемента управления PictureBox и Label, используемого как Button
                                    // Выполняет сохранение данных заявки в Базу данных 
                                    PictureBox Pic_Save = new PictureBox { BackgroundImage = Properties.Resources.Picture_Save, BackgroundImageLayout = ImageLayout.Stretch, Size = new Size(40, 40), BorderStyle = BorderStyle.FixedSingle };
                                    Label Lab_Save = new Label { Name = Request.ID, Text = "Сохранить", Font = new Font("Times New Roman", 16), Size = new Size(115, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };
                                    Lab_Save.Left = Pic_Delete.Left - Lab_Save.Size.Width; Pic_Save.Left = Lab_Save.Left - Pic_Save.Size.Width; Lab_Save.Click += RequestSave_Click;
                                    Frame2.Controls.Add(Direction); Frame2.Controls.Add(Pic_Delete); Frame2.Controls.Add(Lab_Delete); Frame2.Controls.Add(Pic_Delete); Frame2.Controls.Add(Lab_Delete);
                                    Direction.Size = new Size(Direction.Size.Width - (Lab_Delete.Width + Pic_Delete.Width) * 2 - 20, Direction.Size.Height); Frame2.Controls.Add(Pic_Save); Frame2.Controls.Add(Lab_Save); NewRequest.Controls.Add(Frame2);
                                }
                                // Создание элемента управления Panel содержащего данные заявки для подгруппы #3
                                // Содержит данные ученика, такие как: Фамилия, имя, отчество, номер телефона, день рождения, электронная почта
                                Panel Frame3 = new Panel { Size = new Size(NewRequest.Width / 2, 150), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height + Frame2.Height };
                                {
                                    // Создание элемента управления Label. Заголовок
                                    Label Student = new Label { Text = $"Данные ученика", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 2, 40), TextAlign = ContentAlignment.MiddleCenter, Left = 10 };
                                    // Создание элемента управления Label содержащего фамилию, имя, отчество ученика
                                    Label FIO = new Label { Text = $"ФИО: {Request.Contact_Student.LastName} {Request.Contact_Student.FirstName} {Request.Contact_Student.MiddleName}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 60, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 40 };
                                    // Создание элемента управления Label содержащего номер телефона ученика для связи
                                    Label Telephone1 = new Label { Text = $"Номер телефона: ", Font = new Font("Times New Roman", 16), Size = new Size(180, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 70 };
                                    Label Telephone2 = new Label { Text = $"{Request.Contact_Student.Telephone}", Font = new Font("Times New Roman", 16), Size = new Size(282, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Telephone1.Width, Top = 70, ForeColor = Color.Blue };
                                    // Создание элемента управления Label содержащего дату рождения ученика
                                    Label Birthday1 = new Label { Text = $"Дата рождения: ", Font = new Font("Times New Roman", 16), Size = new Size(180, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 95 };
                                    Label Birthday2 = new Label { Text = $"{Request.Contact_Student.Birthday}", Font = new Font("Times New Roman", 16), Size = new Size(282, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Birthday1.Width, Top = 95, ForeColor = Color.Blue };
                                    // Создание элемента управления Label содержащего электронную почту ученика для связи
                                    Label Email1 = new Label { Text = $"E-mail: ", Font = new Font("Times New Roman", 16), Size = new Size(80, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 120 };
                                    Label Email2 = new Label { Text = $"{Request.Contact_Student.Email}", Font = new Font("Times New Roman", 16), Size = new Size(382, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + Email1.Width, Top = 120, ForeColor = Color.Blue };
                                    Frame3.Controls.Add(Student); Frame3.Controls.Add(FIO); Frame3.Controls.Add(Telephone1); Frame3.Controls.Add(Telephone2); Frame3.Controls.Add(Birthday1); Frame3.Controls.Add(Birthday2); Frame3.Controls.Add(Email1); Frame3.Controls.Add(Email2); NewRequest.Controls.Add(Frame3);
                                }
                                // Создание элемента управления Panel содержащего данные заявки для подгруппы #4
                                //  Содержит данные мастера, такие как: Фамилия, имя, отчество, номер телефона, электронная почта
                                Panel Frame4 = new Panel { Size = new Size(NewRequest.Width / 2, 150), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height + Frame2.Height, Left = NewRequest.Width / 2 };
                                {
                                    // Создание элемента управления Label. Заголовок
                                    Label Teach_Lab = new Label { Text = $"Данные преподавателя", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 2, 40), TextAlign = ContentAlignment.MiddleCenter, Left = 10 };
                                    // Условие «Назначен ли преподаватель»
                                    if (Request.Contact_Teach.LastName != "NULL")
                                    {
                                        //Создание элемента управления Label содержащего фамилию и инициалы мастера
                                        Label FIO = new Label { Text = $"ФИО: {Request.Contact_Teach.LastName} {Request.Contact_Teach.FirstName} {Request.Contact_Teach.MiddleName}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 60, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 40 };
                                        // Создание элемента управления Label содержащего номер телефона преподавателя
                                        Label Telephone1 = new Label { Text = $"Номер телефона: ", Font = new Font("Times New Roman", 16), Size = new Size(180, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 70 };
                                        Label Telephone2 = new Label { Text = $"{Request.Contact_Teach.Telephone}", Font = new Font("Times New Roman", 16), Size = new Size(282, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Telephone1.Width, Top = 70, ForeColor = Color.Blue, };
                                        // Создание элемента управления Label содержащего электронную почту преподавателя
                                        Label Email1 = new Label { Text = $"E-mail: ", Font = new Font("Times New Roman", 16), Size = new Size(80, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 100 };
                                        Label Email2 = new Label { Text = $"{Request.Contact_Teach.Email}", Font = new Font("Times New Roman", 16), Size = new Size(382, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Email1.Width, Top = 100, ForeColor = Color.Blue };
                                        Frame4.Controls.Add(FIO); Frame4.Controls.Add(Telephone1); Frame4.Controls.Add(Telephone2); Frame4.Controls.Add(Email1); Frame4.Controls.Add(Email2);
                                    }
                                    else
                                    {
                                        // Создание элемента управления Label. Сообщение об том, что преподаватель не назначен
                                        ComboBox CB_Teachs = new ComboBox { Name = $"{Request.ID}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 120, 50), Left = 60, Top = 45 };
                                        CB_Teachs.Text = "Не Назначен"; foreach (Contacts Teach in Teach) CB_Teachs.Items.Add($"{Teach.LastName} {Teach.FirstName} {Teach.MiddleName}");
                                        // Создание элемента управления Label, используемого как Button
                                        //Выполняет сохранение данных преподавателя в заявку в Базе данных
                                        Label TeachSet = new Label { Text = "Назначить преподавателя", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 160, 46), TextAlign = ContentAlignment.MiddleCenter, Left = 80, Top = 88, BorderStyle = BorderStyle.FixedSingle };
                                        Frame4.Controls.Add(CB_Teachs); TeachSet.Click += TeachSet_Click; Frame4.Controls.Add(TeachSet); CB_Tch = CB_Teachs;
                                    }
                                    Frame4.Controls.Add(Teach_Lab); NewRequest.Controls.Add(Frame4);
                                }
                                Table.Controls.Add(NewRequest);
                            }
                        }
            Table.AutoScroll = true;
        }
        //  Обработка нажатия кнопки «Сохранить».
        //  Выполняет сохранение данных заявки в Базу данных 
        private void RequestSave_Click(object sender, EventArgs e)
        {
            string[] Named = new string[4]; Label Click = (Label)sender;
            // Обработка исключений «Нажатый элемент управления»
            foreach (Panel Control in Table.Controls) if (Control.Name[0] == 'R' & Control.Name.Remove(0, 2) == Click.Name)
            {
                    Named[0] = Control.Name.Remove(0, 2); foreach (Panel Frame in Control.Controls) if (Frame.Name == "Frame1") foreach (var Element in Frame.Controls)
                    {
                           switch (Element.GetType().ToString())
                           {
                                case "System.Windows.Forms.Label":
                                {
                                      Label Element_Label = (Label)Element;
                                      if (Element_Label.Name == "Cost") { Named[1] = ""; string[] G = Element_Label.Text.Split(' '); for (int i = 2; i < G.Length - 1; i++) Named[1] += G[i]; }
                                      if (Element_Label.Name == "TrainingStatuses") Named[2] = Element_Label.Text; if (Element_Label.Name == "PaymentStatuses") Named[3] = Element_Label.Text;
                                }
                                break;
                                case "System.Windows.Forms.MaskedTextBox":
                                {
                                      MaskedTextBox Element_MaskedTextBox = (MaskedTextBox)Element;
                                      if (Element_MaskedTextBox.Name == "Cost") { Named[1] = ""; string[] G = Element_MaskedTextBox.Text.Split(' '); for (int i = 0; i < G.Length - 1; i++) Named[1] += G[i]; }
                                      if (Element_MaskedTextBox.Name == "StatusTraining") Named[2] = Element_MaskedTextBox.Text; if (Element_MaskedTextBox.Name == "PaymentStatuses") Named[3] = Element_MaskedTextBox.Text;
                                }
                                break;
                                case "System.Windows.Forms.ComboBox":
                                {
                                      ComboBox Element_ComboBox = (ComboBox)Element;
                                      if (Element_ComboBox.Name == "Cost") { Named[1] = ""; string[] G = Element_ComboBox.Text.Split(' '); for (int i = 0; i < G.Length - 1; i++) Named[1] += G[i]; }
                                      if (Element_ComboBox.Name == "StatusTraining") Named[2] = Element_ComboBox.Text; if (Element_ComboBox.Name == "StatusPayment") Named[3] = Element_ComboBox.Text;
                                }
                                break;
                           }
                    }
            }
            int Cost = 0; if (Named[1] != "") Cost = Convert.ToInt32(Named[1]);
            // Выполнение хранимой процедуры
            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[InformationTraining_Update] @RequestId, @Cost, @TrainingStatuses, @PaymentStatuses"; // SQL-запрос
                SQL_Command.Parameters.Add("@RequestId", SqlDbType.VarChar, 8); SQL_Command.Parameters["@RequestId"].Value = Named[0];
                SQL_Command.Parameters.Add("@Cost", SqlDbType.Int); SQL_Command.Parameters["@Cost"].Value = Cost;
                SQL_Command.Parameters.Add("@TrainingStatuses", SqlDbType.NVarChar, 16); SQL_Command.Parameters["@TrainingStatuses"].Value = Named[2];
                SQL_Command.Parameters.Add("@PaymentStatuses", SqlDbType.NVarChar, 12); SQL_Command.Parameters["@PaymentStatuses"].Value = Named[3];
                SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
            }
            // Сообщение об успешном сохранении указанных данных
            MessageBox.Show("Данные успешно обновлены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); AdminAccount_Load(sender, e);
        }
        //  Обработка нажатия кнопки «Удалить».
        //  Выполняет удаление выбранной заявки после повторного подтверждения
        private void RequestDelete_Click(object sender, EventArgs e)
        {
            // Подтверждение удаления
            if (MessageBox.Show("Уверены, что хотите удалить заявку?", "Подтверждение", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string Named = ""; Label Click = (Label)sender;
                foreach (Panel Control in Table.Controls) if (Control.Name[0] == 'R' & Control.Name.Remove(0, 2) == Click.Name) Named = Control.Name.Remove(0, 2);
                // Получение данных согласно выполненной хранимой процедуры
                using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
                {
                    SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                    string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[InformationTraining_Delete] @RequestId"; // SQL-запрос
                    SQL_Command.Parameters.Add("@RequestId", SqlDbType.VarChar, 8); SQL_Command.Parameters["@RequestId"].Value = Named;
                    SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                }
                // Сообщение об успешном удалении указанных данных
                MessageBox.Show("Данные успешно удалены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); AdminAccount_Load(sender, e);
            }
        }
        // Переменная. Содержит элемент управления ComboBox, включающий в себя список мастеров и их данных
        private ComboBox CB_Tch;
        private void TeachSet_Click(object sender, EventArgs e)
        {
            // Выполнение хранимой процедуры
            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[Teach Set] @RequestId, @DirectionTeach"; // SQL-запрос
                SQL_Command.Parameters.Add("@RequestId", SqlDbType.VarChar, 8); SQL_Command.Parameters["@RequestId"].Value = CB_Tch.Name;
                SQL_Command.Parameters.Add("@DirectionTeach", SqlDbType.VarChar, 7); SQL_Command.Parameters["@DirectionTeacher"].Value = Teach[CB_Tch.SelectedIndex].ID;
                SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
            }
            // Сообщение об успешном сохранении указанных данных
            MessageBox.Show("Мастер успешно назначен", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); AdminAccount_Load(sender, e);
        }

        private bool UpdateT = false;
        // Обработка изменения выбранного значения в элементах управления «CB_Student» и «CB_StatusTraining»
        private void CB_SelectedIndexChanged(object sender, EventArgs e) { if (UpdateT) UpdateOut(); }
        // Обработка изменения размера формы
        private void AdminAccount_Resize(object sender, EventArgs e) { if (UpdateT) UpdateOut(); }
    }
}
