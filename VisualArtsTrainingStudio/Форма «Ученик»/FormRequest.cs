using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisualArtsTrainingStudio.Форма__Ученик_
{
    public partial class FormRequest : Form
    {
        public FormRequest() => InitializeComponent();
        // Переменная. Строка подключения
        public static string ConnectString = @"Data Source='';Integrated Security=True";
        // Переменная. Авторизирован пользователь или нет
        public static bool Request_Authorized { get; set; }
        // Переменная. Содержит ID контактных данных ученика
        public static string Contacts { get; set; }
        // Обработка первоначальной загрузки формы
        private void Request_Load(object sender, EventArgs e)
        { if (Request_Authorized) Size = new Size(splitContainer1.SplitterDistance + 10, Height); }

        // Обработка нажатия кнопки «Отправить заявку».
        // Выполняет добавление новых данных в Базу данных

        private void SendRequest_Click(object sender, EventArgs e)
        {
            // Условие «Авторизирован пользователь или нет»
            if (Request_Authorized)
                // Обработка исключений «Пустые поля ввода»
                if (TB_Direction.Text != "")
                {
                    Contacts Сontact = null;

                    // Получение данных согласно выполненной хранимой процедуры
                    using (SqlConnection SQL_Connection = new SqlConnection(ConnectString))
                    {
                        SQL_Connection.Open();
                        string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[Contacts_Id] @Id"; // SQL-запрос
                        SqlCommand SQL_Command = new SqlCommand(Request, SQL_Connection);
                        SQL_Command.Parameters.Add("@Id", SqlDbType.VarChar, 7); SQL_Command.Parameters["@Id"].Value = Contacts; SqlDataReader Reader = SQL_Command.ExecuteReader();
                        while (Reader.Read()) Сontact = new Contacts((string)Reader.GetValue(0), (string)Reader.GetValue(1), (string)Reader.GetValue(2), (string)Reader.GetValue(3), (string)Reader.GetValue(4), (DateTime)Reader.GetValue(5));
                        SQL_Connection.Close();
                    }

                    // Добавление новых данных в Базу данных согласно выполненной хранимой процедуры
                    using (SqlConnection SQL_Connection = new SqlConnection(ConnectString))
                    {
                        SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                        string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[Request_ADD] @LastName, @FirstName, @MiddleName, @Email, @Telephone, @Birthday, @Direction, @Date"; // SQL-запрос
                        SQL_Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@LastName"].Value = Сontact.LastName;
                        SQL_Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@FirstName"].Value = Сontact.FirstName;
                        SQL_Command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@MiddleName"].Value = Сontact.MiddleName;
                        SQL_Command.Parameters.Add("@Telephone", SqlDbType.VarChar, 20); SQL_Command.Parameters["@Telephone"].Value = Сontact.Telephone;
                        SQL_Command.Parameters.Add("@Birthday", SqlDbType.VarChar, 10); SQL_Command.Parameters["@Birthday"].Value = Сontact.Birthday;
                        SQL_Command.Parameters.Add("@Email", SqlDbType.VarChar, 256); SQL_Command.Parameters["@Email"].Value = Сontact.Email;
                        SQL_Command.Parameters.Add("@Direction", SqlDbType.NVarChar, 50); SQL_Command.Parameters["@Direction"].Value = TB_Direction.Text;
                        SQL_Command.Parameters.Add("@Date", SqlDbType.Date); SQL_Command.Parameters["@Date"].Value = DateTime.Now.Date;
                        SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                    }
                }
                else  // Обработка исключений «Пустые поля ввода»
                if (TB_Direction.Text != "" & (TB_Telephone.Text != "" | TB_Birthday.Text != "" | TB_Email.Text != "") & TB_LastName.Text != "" & TB_FirstName.Text != "" & TB_MiddleName.Text != "")
                {
                    // Добавление новых данных в Базу данных согласно выполненной хранимой процедуры
                    using (SqlConnection SQL_Connection = new SqlConnection(ConnectString))
                    {
                        SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                        string Request = $"EXEC [VisualArtsTrainingStudio].[dbo].[Request_ADD] @LastName, @FirstName, @MiddleName, @Email, @Birthday, @Telephone, @Direction, @Date"; // SQL-запрос
                        SQL_Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@LastName"].Value = TB_LastName.Text;
                        SQL_Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@FirstName"].Value = TB_FirstName.Text;
                        SQL_Command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 24); SQL_Command.Parameters["@MiddleName"].Value = TB_MiddleName.Text;
                        SQL_Command.Parameters.Add("@Telephone", SqlDbType.VarChar, 20); SQL_Command.Parameters["@Telephone"].Value = TB_Telephone.Text;
                        SQL_Command.Parameters.Add("@Birthday", SqlDbType.VarChar, 10); SQL_Command.Parameters["@Birthday"].Value = TB_Birthday.Text;
                        SQL_Command.Parameters.Add("@Email", SqlDbType.VarChar, 256); SQL_Command.Parameters["@Email"].Value = TB_Email.Text;
                        SQL_Command.Parameters.Add("@Direction", SqlDbType.NVarChar, 50); SQL_Command.Parameters["@Direction"].Value = TB_Direction.Text;
                        SQL_Command.Parameters.Add("@Date", SqlDbType.Date); SQL_Command.Parameters["@Date"].Value = DateTime.Now.Date;
                        SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
                    }
                }

            // Сообщение об успешном добавление новых данных в Базу данных
            MessageBox.Show("Заявка успешно отправлена", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information); Close();

        }

        private void TB_FirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
