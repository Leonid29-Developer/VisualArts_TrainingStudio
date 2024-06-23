using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using VisualArtsTrainingStudio.Форма__Ученик_;


namespace VisualArtsTrainingStudio
{
    public partial class StudentAccount : Form
    {
        public StudentAccount() => InitializeComponent();
        // Переменная. Содержит ID контактных данных преподавателя
        public static string Contacts { get; set; }
        // Коллекция содержащая данные о заявках
        private List<InformationTraining> Requests = new List<InformationTraining>();
        // Обработка первоначальной загрузки формы
        private void StudentAccount_Load(object sender, EventArgs e)
        {
            Requests.Clear(); Table.Controls.Clear();
            // Выполнение хранимых процедур и заполнение коллекции «Requests»
            using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
            {
                SQL_Connection.Open(); string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[InformationTraining_Contacts] @Id"; // SQL-запрос
                SqlCommand SQL_Command = new SqlCommand(Request, SQL_Connection);
                SQL_Command.Parameters.Add("@Id", SqlDbType.VarChar, 7); SQL_Command.Parameters["@Id"].Value = Contacts; SqlDataReader Reader = SQL_Command.ExecuteReader();
                while (Reader.Read())
                {
                    int COST = 0; if (Reader.GetValue(2) != DBNull.Value) COST = (int)Reader.GetValue(2); Contacts Сontact = null;

                    // Получение данных согласно выполненной хранимой процедуры
                    if (Reader.GetValue(5) != DBNull.Value)
                        using (SqlConnection SQL_Connection2 = new SqlConnection(FormRequest.ConnectString))
                        {
                            SQL_Connection2.Open();
                            Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[Сontacts_Id] @Id"; // SQL-запрос
                            SqlCommand SQL_Command2 = new SqlCommand(Request, SQL_Connection2);
                            SQL_Command2.Parameters.Add("@Id", SqlDbType.VarChar, 7); SQL_Command2.Parameters["@Id"].Value = (string)Reader.GetValue(5); SqlDataReader Reader2 = SQL_Command2.ExecuteReader();
                            while (Reader2.Read()) Сontact = new Contacts((string)Reader2.GetValue(0), (string)Reader2.GetValue(1), (string)Reader2.GetValue(2), (string)Reader2.GetValue(3), (string)Reader2.GetValue(4), (DateTime)Reader2.GetValue(5));
                            SQL_Connection2.Close();
                        }

                    
                    Requests.Add(new InformationTraining((string)Reader.GetValue(0), (string)Reader.GetValue(1),  COST, (string)Reader.GetValue(3), (string)Reader.GetValue(4), null, Сontact, (DateTime)Reader.GetValue(6)));
                }
                SQL_Connection.Close();
            }
            // Создание элемента управления Panel. Границы заголовка
            Panel Head_CR = new Panel { Size = new Size(Table.Width - 40, 80) };
            {
                // Создание элемента управления Label. Заголовок
                Label CurrentRequests = new Label { Font = new Font("Times New Roman", 22), Size = new Size(Head_CR.Width - 150, Head_CR.Height), Text = "       Текущие заявки", TextAlign = ContentAlignment.MiddleLeft, Left = 50, BorderStyle = BorderStyle.FixedSingle };
                Label Arrow = new Label { Font = new Font("Times New Roman", 22), Size = new Size(80, Head_CR.Height), Text = "▼", TextAlign = ContentAlignment.MiddleCenter, Left = Head_CR.Width - 99, BorderStyle = BorderStyle.FixedSingle };
                Head_CR.Controls.Add(CurrentRequests); Head_CR.Controls.Add(Arrow); Table.Controls.Add(Head_CR);
            }
            // Обработка исключения «Данные отсутствуют»
            if (Requests.Count > 0)
                // Перебор коллекции «Requests» и вывод данных из неё согласно заданным условиям
                foreach (InformationTraining Request in Requests)
                    // Условие «Совпадание статуса обучения»
                    if (Request.StatusTraining != "Обучается" & Request.StatusTraining != "Отчислен")
                    {
                        // Создание элемента управления Panel содержащего данные заявки разделенные на подгруппы
                        Panel NewRequest = new Panel { Size = new Size(Table.Width - 40, 160), BorderStyle = BorderStyle.FixedSingle };
                        {
                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #1
                            // Содержит данные заявки, такие как: номер, дата, статус работы, статус оплаты, стоимость
                            Panel Frame1 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60, BorderStyle = BorderStyle.FixedSingle };
                            {
                                // Создание элемента управления Label содержащего номер и дату заявки
                                Label Number_Date = new Label { Text = $"Заявка №{Request.ID.Remove(0, 2)} от {Request.Date.Day}.{Request.Date.Month}.{Request.Date.Year}", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 5 };

                                // Создание элемента управления Label содержащего стоимость занятия
                                Label Cost = new Label { Text = $"Стоимость: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width * 2 / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 40 };
                                if (Request.Cost == 0) Cost.Text += $"Не назначено"; else Cost.Text += $"{Request.Cost} руб.";

                                // Создание элемента управления Label содержащего статус обучения
                                Label StatusTraining1 = new Label { Text = $"Статус обучения: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 80 };
                                Label StatusTraining2 = new Label { Text = $"{Request.StatusTraining}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + NewRequest.Width / 7, Top = 80 };
                                if (Request.StatusTraining == "Обучается") StatusTraining2.ForeColor = Color.Green; else StatusTraining2.ForeColor = Color.Orange;

                                // Создание элемента управления Label содержащего статус оплаты
                                Label StatusPayment1 = new Label { Text = $"Статус оплаты: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 120 };
                                Label StatusPayment2 = new Label { Text = $"{Request.StatusPayment}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + NewRequest.Width / 7, Top = 120, ForeColor = Color.Green };

                                Frame1.Controls.Add(Number_Date); Frame1.Controls.Add(StatusTraining1); Frame1.Controls.Add(StatusTraining2); Frame1.Controls.Add(Cost); Frame1.Controls.Add(StatusPayment1); Frame1.Controls.Add(StatusPayment2); NewRequest.Controls.Add(Frame1);
                            }
                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #2
                            // Содержит данные заявки, такие как: направление
                            Panel Frame2 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60 + NewRequest.Width / 3, BorderStyle = BorderStyle.FixedSingle };
                            {
                                // Создание элемента управления Label содержащего направление указанного в заявке
                                Label Direction = new Label { Text = $"Направление: {Request.Direction}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10 };
                                Frame2.Controls.Add(Direction); NewRequest.Controls.Add(Frame2);
                            }
                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #3
                            // Содержит данные преподавателя, такие как: Фамилия и инициалы
                            Panel Frame3 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60 + NewRequest.Width * 2 / 3, BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Teach1 = new Label { Text = $"Преподаватель: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 12, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10, Top = 50 };
                                // Создание элемента управления Label содержащего Фамилию и инициалы преподавателя
                                Label Teach2 = new Label { Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width * 3 / 16, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10 + Teach1.Width, Top = 50 };
                                if (Request.Contact_Teach == null) Teach2.Text = $"Не назначен"; else Teach2.Text = $"{Request.Contact_Teach.LastName} {Request.Contact_Teach.FirstName[0]}. {Request.Contact_Teach.MiddleName[0]}.";
                                Frame3.Controls.Add(Teach1); Frame3.Controls.Add(Teach2); NewRequest.Controls.Add(Frame3);
                            }
                            Table.Controls.Add(NewRequest);
                        }
                    }

            // Создание элемента управления Panel. Границы заголовка
            Panel Head_HR = new Panel { Size = new Size(Table.Width - 40, 80) };
            {
                // Создание элемента управления Label. Заголовок
                Label HistoryOfRequests = new Label { Font = new Font("Times New Roman", 22), Size = new Size(Head_CR.Width - 150, Head_CR.Height), Text = "       История занятий", TextAlign = ContentAlignment.MiddleLeft, Left = 50, BorderStyle = BorderStyle.FixedSingle };
                Label Arrow = new Label { Font = new Font("Times New Roman", 22), Size = new Size(80, Head_CR.Height), Text = "▼", TextAlign = ContentAlignment.MiddleCenter, Left = Head_CR.Width - 99, BorderStyle = BorderStyle.FixedSingle };

                Head_HR.Controls.Add(HistoryOfRequests); Head_HR.Controls.Add(Arrow); Table.Controls.Add(Head_HR);
            }

            // Обработка исключения «Данные отсутствуют»
            if (Requests.Count > 0)
                // Перебор коллекции «Requests» и вывод данных из неё согласно заданным условиям
                foreach (InformationTraining Request in Requests)
                    // Условие «Совпадание статуса обучения»
                    if (Request.StatusTraining == "Обучается" | Request.StatusTraining == "Отклонено")
                    {
                        // Создание элемента управления Panel содержащего данные заявки разделенные на подгруппы
                        Panel NewRequest = new Panel { Size = new Size(Table.Width - 40, 160), BorderStyle = BorderStyle.FixedSingle };
                        {
                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #1
                            // Содержит данные заявки, такие как: номер, дата, статус обучения, статус оплаты, стоимость
                            Panel Frame1 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60, BorderStyle = BorderStyle.FixedSingle };
                            {
                                // Создание элемента управления Label содержащего номер и дату заявки
                                Label Number_Date = new Label { Text = $"Заявка №{Request.ID.Remove(0, 2)} от {Request.Date.Day}.{Request.Date.Month}.{Request.Date.Year}", Font = new Font("Times New Roman", 17), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 5 };

                                // Создание элемента управления Label содержащего стоимость занятия
                                Label Cost = new Label { Text = $"Стоимость: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width * 2 / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 40 };
                                if (Request.Cost == 0) Cost.Text += $"Не назначено"; else Cost.Text += $"{Request.Cost} руб.";

                                // Создание элемента управления Label содержащего статус обучения
                                Label StatusTraining1 = new Label { Text = $"Статус обучения: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 80 };
                                Label StatusTraining2 = new Label { Text = $"{Request.StatusTraining}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + NewRequest.Width / 7, Top = 80 };
                                if (Request.StatusTraining == "Отчислен") StatusTraining2.ForeColor = Color.Red; else StatusTraining2.ForeColor = Color.Green;

                                // Создание элемента управления Label содержащего статус оплаты заявки
                                Label StatusPayment1 = new Label { Text = $"Статус оплаты: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20, Top = 120 };
                                Label StatusPayment2 = new Label { Text = $"{Request.StatusPayment}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 7, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 20 + NewRequest.Width / 7, Top = 120, ForeColor = Color.Green };

                                Frame1.Controls.Add(Number_Date); Frame1.Controls.Add(StatusTraining1); Frame1.Controls.Add(StatusTraining2); Frame1.Controls.Add(Cost); Frame1.Controls.Add(StatusPayment1); Frame1.Controls.Add(StatusPayment2); NewRequest.Controls.Add(Frame1);
                            }

                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #2
                            // Содержит данные заявки, такие как: направление
                            Panel Frame2 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60 + NewRequest.Width / 3, BorderStyle = BorderStyle.FixedSingle };
                            {
                                // Создание элемента управления Label содержащего направление указанного в заявке
                                Label Direction = new Label { Text = $"Направление: {Request.Direction}", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 3, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10 };
                                Frame2.Controls.Add(Direction); NewRequest.Controls.Add(Frame2);
                            }

                            // Создание элемента управления Panel содержащего данные заявки для подгруппы #3
                            // Содержит данные преподавателя, такие как: Фамилия и инициалы
                            Panel Frame3 = new Panel { Size = new Size(NewRequest.Width / 3, 160), Left = 60 + NewRequest.Width * 2 / 3, BorderStyle = BorderStyle.FixedSingle };
                            {
                                Label Teach1 = new Label { Text = $"Преподаватель: ", Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width / 12, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10, Top = 50 };
                                // Создание элемента управления Label содержащего Фамилию и инициалы преподавателя
                                Label Teach2 = new Label { Font = new Font("Times New Roman", 16), Size = new Size(NewRequest.Width * 3 / 16, 40), TextAlign = ContentAlignment.MiddleLeft, Left = 10 + Teach1.Width, Top = 50 };
                                if (Request.Contact_Teach == null) Teach2.Text = $"Не назначен"; else Teach2.Text = $"{Request.Contact_Teach.LastName} {Request.Contact_Teach.FirstName[0]}. {Request.Contact_Teach.MiddleName[0]}.";
                                Frame3.Controls.Add(Teach1); Frame3.Controls.Add(Teach2); NewRequest.Controls.Add(Frame3);
                            }

                            Table.Controls.Add(NewRequest);
                        }
                    }
            Table.AutoScroll = true;
        }

        // Обработка нажатия кнопки «Составить новую заявку».
        // Выполняет открытие формы «FormRequest».
        private void Label_Request_Click(object sender, EventArgs e)
        { FormRequest.Contacts = Contacts; FormRequest.Request_Authorized = true; Hide(); new FormRequest().ShowDialog(); Show(); StudentAccount_Load(sender, e); }
    }
}
