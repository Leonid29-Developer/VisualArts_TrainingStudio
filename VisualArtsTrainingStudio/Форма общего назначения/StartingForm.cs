using System;
using System.Windows.Forms;
using VisualArtsTrainingStudio.Форма__Ученик_;

namespace VisualArtsTrainingStudio
{
    public partial class StartingForm : Form
    {
        public StartingForm() => InitializeComponent();

        // Обработка нажатия на кнопку «Request». Открытие формы «FormRequest»
        private void Request_Click(object sender, EventArgs e)
        { Hide(); FormRequest.Request_Authorized = false; new FormRequest().ShowDialog(); Show(); }

        // Обработка нажатия на кнопку «Authorization». Открытие формы «Authorization»
        private void Authorization_Click(object sender, EventArgs e)
        { Hide(); new Authorization().ShowDialog(); Show(); }
    }
}
