using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using VisualArtsTrainingStudio.Форма__Ученик_;

namespace VisualArtsTrainingStudio
{
    public partial class Authorization : Form
    {
        public Authorization() => InitializeComponent();
        // Обработка нажатия кнопки «Button_Input». 
        // Открытие форм «StudentAccount», «TeachAccount», «AdminAccount» согласно правам доступа после успешной аутентификации
        private void Button_Input_Click(object sender, EventArgs e)
        {
            bool T = false;
            if (TB_Login.Text != "" & TB_Password.Text != "")
            {
                DataTable DATA = new DataTable(); // Локальная таблица данных

                // Получение данных согласно выполненной хранимой процедуры
                using (SqlConnection SQL_Connection = new SqlConnection(FormRequest.ConnectString))
                {
                    SQL_Connection.Open(); using (SqlCommand CMD = new SqlCommand($"EXEC [VisualArtsTrainingStudio].[dbo].[Authorization] '{TB_Login.Text}','{TB_Password.Text}'", SQL_Connection))
                    using (SqlDataReader Reader = CMD.ExecuteReader()) { T = Reader.HasRows; DATA.Load(Reader); }
                    SQL_Connection.Close();
                }
                // Обработка аутентификации. Открытие форм согласно правам доступа
                if (T == true) switch ((string)DATA.Rows[0][0])
                    {
                        // Открытие формы «StudentAccount» для права доступа «Student»
                        case "Student": { Hide(); StudentAccount.Contacts = (string)DATA.Rows[0][1]; new StudentAccount().ShowDialog(); Close(); } break;
                        // Открытие формы «TeachAccount» для права доступа «Teacher»
                        case "Teacher": { Hide(); TeachAccount.Contacts = (string)DATA.Rows[0][1]; new TeachAccount().ShowDialog(); Close(); } break;
                        // Открытие формы «AdminAccount» для права доступа «Administrator»
                        case "Administrator": { Hide(); new AdminAccount().ShowDialog(); Close(); } break;
                    }
            }
            else T = false; if (T == false) MessageBox.Show("Неверные данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        // Обработка закрытия формы
        private void Authorization_FormClosed(object sender, FormClosedEventArgs e) => Application.OpenForms["StartingForm"].Show();

        // Кнопки быстрой авторизации
        private void pictureBox1_Click(object sender, EventArgs e) { TB_Login.Text = "Student"; TB_Password.Text = "11111"; }
        private void pictureBox2_Click(object sender, EventArgs e) { TB_Login.Text = "Teacher"; TB_Password.Text = "22222"; }
        private void pictureBox3_Click(object sender, EventArgs e) { TB_Login.Text = "Admin"; TB_Password.Text = "33333"; }
    }
}
