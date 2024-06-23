using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using VisualArtsTrainingStudio.Форма__Ученик_;

namespace VisualArtsTrainingStudio
{
    public partial class TeachAccount : Form
    {
        public TeachAccount() => InitializeComponent();

        // Переменная. Содержит ID контактных данных мастера, проводящего ТО
        public static string Contacts { get; set; }
        // Коллекция содержащая данные о заявках
        private List<InformationTraining> Requests = new List<InformationTraining>();

        // Обработка первоначальной загрузки формы
        private void TeachAccount_Load(object sender, EventArgs e)
        {
            Requests.Clear();

            // Выполнение хранимых процедур и заполнение коллекции «Requests»
            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
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
                        while (Reader2.Read()) Contact_Student = new Contacts
                                 ((string)Reader2.GetValue(0), (string)Reader2.GetValue(1), (string)Reader2.GetValue(2), (string)Reader2.GetValue(5), (string)Reader2.GetValue(3), (DateTime)Reader2.GetValue(4));
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
                                Contact_Teach = new Contacts
                                    ((string)Reader2.GetValue(0), (string)Reader2.GetValue(1), (string)Reader2.GetValue(2), (string)Reader2.GetValue(5), (string)Reader2.GetValue(3), (DateTime)Reader2.GetValue(4));
                                Contact_Teach.IDA((string)Reader.GetValue(6));
                            }
                            SQL_Connection2.Close();
                        }

                    Requests.Add(new InformationTraining
                        ((string)Reader.GetValue(0), (string)Reader.GetValue(2), COST, (string)Reader.GetValue(4), (string)Reader.GetValue(5), Contact_Student, Contact_Teach, (DateTime)Reader.GetValue(7)));
                }
                SQL_Connection.Close();
            }

            UpdateOut();
        }
        // Очистка интерфейса. Создание элементов управления согласно данным из коллекций и вывод их на интрефейсе
        private void UpdateOut()
        {
            Table.Controls.Clear();

            // Обработка исключения «Данные отсутствуют»
            if (Requests.Count > 0)
                // Перебор коллекции «Requests» и вывод данных из неё согласно заданным условиям
                foreach (InformationTraining Request in Requests)
                    // Условие «Совпадание ID преподавателя и статуса работы записи»
                    if (Request.Contact_Teach.ID == Contacts & Request.StatusTraining == "Обучается")
                    {
                        // Создание элемента управления Panel содержащего данные заявки разделенные на подгруппы
                        Panel NewRequest = new Panel { Name = $"R_{Request.ID}", Size = new Size(Table.Width, 352), BorderStyle = BorderStyle.FixedSingle };
                        {
                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #1
                            // Содержит данные заявки, такие как: номер, дата, стоимость
                            Panel Frame1 = new Panel { Name = "Frame1", Size = new Size(NewRequest.Width, 110), BorderStyle = BorderStyle.FixedSingle };
                            {
                                // Создание элемента управления Label содержащего номер и дату заявки
                                Label Number_Date = new Label { Text = $"Заявка №{Request.ID.Remove(0, 2)} от {Request.Date.Day}.{Request.Date.Month}.{Request.Date.Year}", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width, 40), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };

                                // Создание элемента управления Label содержащего стоимость заявки
                                Label Cost = new Label { Text = $"Предварительная cтоимость занятия: ", Font = new Font("Times New Roman", 18), Size = new Size(390, 50), TextAlign = ContentAlignment.MiddleLeft, Left = 80, Top = 48 };

                                // Создание элемента управления MaskedTextBox для ввода пользователем новой стоимости
                                MaskedTextBox TB_Cost = new MaskedTextBox { Name = "Cost", Font = new Font("Times New Roman", 19), TextAlign = HorizontalAlignment.Center, Mask = "000 000 000 руб", Size = new Size(190, 50), Left = Cost.Left + Cost.Width, Top = 56 };
                                if (Request.Cost != 0) TB_Cost.Text = Request.Cost.ToString(); else TB_Cost.Text = "00000";
                                // Создание элемента управления PictureBox и Label, используемого как Button
                                // Выполняет сохранение стоимости заявки в Базу данных 
                                PictureBox Pic_UpdateCost = new PictureBox { BackgroundImage = Properties.Resources.Picture_Notepad, BackgroundImageLayout = ImageLayout.Stretch, Size = new Size(50, 50), Left = 750, Top = 48, BorderStyle = BorderStyle.FixedSingle };
                                Label Lab_UpdateCost = new Label { Name = Request.ID, Text = "Зафиксировать", Font = new Font("Times New Roman", 18), Size = new Size(180, 50), TextAlign = ContentAlignment.MiddleCenter, Left = Pic_UpdateCost.Left + 50, Top = 48, BorderStyle = BorderStyle.FixedSingle };
                                Lab_UpdateCost.Click += RequestUpdateCost_Click;

                                Frame1.Controls.Add(Number_Date); Frame1.Controls.Add(Cost); Frame1.Controls.Add(TB_Cost); Frame1.Controls.Add(Pic_UpdateCost); Frame1.Controls.Add(Lab_UpdateCost); NewRequest.Controls.Add(Frame1);
                            }

                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #2
                            // Содержит данные заявки, такое как: направление
                            Panel Frame2 = new Panel { Size = new Size(NewRequest.Width, 120), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height };
                            {
                                // Создание элемента управления Label содержащего направление указанное в заявке
                                Label Direction = new Label { Text = $"Направление: {Request.Direction}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width - 55, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 23, BorderStyle = BorderStyle.FixedSingle };

                                Frame2.Controls.Add(Direction); NewRequest.Controls.Add(Frame2);
                            }

                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #3
                            // Содержит данные клиента, такие как: Фамилия и инициалы, номер телефона
                            Panel Frame3 = new Panel { Size = new Size(NewRequest.Width / 2, 120), BorderStyle = BorderStyle.FixedSingle, Top = Frame1.Height + Frame2.Height, Left = NewRequest.Width / 4 };
                            {
                                // Создание элемента управления Label. Заголовок
                                Label Client = new Label { Text = $"Данные ученика", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 2, 40), TextAlign = ContentAlignment.MiddleCenter, Left = 10 };

                                // Создание элемента управления Label содержащего фамилию, имя, отчество клиента
                                Label FIO = new Label { Text = $"Обращение: {Request.Contact_Student.FirstName} {Request.Contact_Student.MiddleName}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 2 - 60, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 40 };

                                // Создание элемента управления Label содержащего номер телефона клиента для связи
                                Label Telephone1 = new Label { Text = $"Номер телефона: ", Font = new Font("Times New Roman", 16), Size = new Size(180, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30, Top = 70 };
                                Label Telephone2 = new Label { Text = $"{Request.Contact_Student.Telephone}", Font = new Font("Times New Roman", 16), Size = new Size(282, 30), TextAlign = ContentAlignment.MiddleLeft, Left = 30 + Telephone1.Width, Top = 70, ForeColor = Color.Blue };

                                Frame3.Controls.Add(Client); Frame3.Controls.Add(FIO); Frame3.Controls.Add(Telephone1); Frame3.Controls.Add(Telephone2); NewRequest.Controls.Add(Frame3);
                            }

                            Table.Controls.Add(NewRequest);
                        }
                    }

            Table.AutoScroll = true;
        }
        // Обработка нажатия кнопки «Зафиксировать».
        // Выполняет сохранение стоимости заявки в Базу данных 
        private void RequestUpdateCost_Click(object sender, EventArgs e)
        {
            string[] Named = new string[2]; Label Click = (Label)sender;

            // Обработка исключений «Нажатый элемент управления»
            foreach (Panel Control in Table.Controls) if (Control.Name[0] == 'R' & Control.Name.Remove(0, 2) == Click.Name)
                {
                    Named[0] = Control.Name.Remove(0, 2); foreach (Panel Frame in Control.Controls) if (Frame.Name == "Frame1") foreach (var Element in Frame.Controls) if (Element.GetType().ToString() == "System.Windows.Forms.MaskedTextBox")
                                {
                                    MaskedTextBox Element_MaskedTextBox = (MaskedTextBox)Element;
                                    if (Element_MaskedTextBox.Name == "Cost") { Named[1] = ""; string[] G = Element_MaskedTextBox.Text.Split(' '); for (int i = 0; i < G.Length - 1; i++) Named[1] += G[i]; }
                                }
                }

            int Cost = 0; if (Named[1] != "") Cost = Convert.ToInt32(Named[1]);

            // Перебор коллекции «Requests»
            foreach (InformationTraining Requested in Requests)
                // Условие «Совпадение номера заявки»
                if (Requested.ID == Named[0])
                    // Выполнение хранимой процедуры
                    using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
                    {
                        SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                        string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[InformationTraining_Update] @RequestId, @Cost, @TrainingStatuses, @PaymentStatuses"; // SQL-запрос
                        SQL_Command.Parameters.Add("@RequestId", SqlDbType.VarChar, 8); SQL_Command.Parameters["@RequestId"].Value = Named[0];
                        SQL_Command.Parameters.Add("@Cost", SqlDbType.Int); SQL_Command.Parameters["@Cost"].Value = Cost;
                        SQL_Command.Parameters.Add("@TrainingStatuses", SqlDbType.NVarChar, 16); SQL_Command.Parameters["@TrainingStatuses"].Value = Requested.StatusTraining;
                        SQL_Command.Parameters.Add("@PaymentStatuses", SqlDbType.NVarChar, 12); SQL_Command.Parameters["@PaymentStatuses"].Value = Requested.StatusPayment;
                        SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                    }

            // Сообщение об успешном сохранении указанных данных
            MessageBox.Show("Данные успешно обновлены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); TeachAccount_Load(sender, e);
        }

        // Обработка изменения размера формы
        private void TeachAccount_Resize(object sender, EventArgs e) => UpdateOut();
    }
}