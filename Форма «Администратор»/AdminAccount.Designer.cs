
namespace VisualArtsTrainingStudio
{
    partial class AdminAccount
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.CB_Student = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_StatusTraining = new System.Windows.Forms.ComboBox();
            this.Table = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.label1.Location = new System.Drawing.Point(167, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ученик";
            // 
            // CB_Student
            // 
            this.CB_Student.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CB_Student.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.CB_Student.FormattingEnabled = true;
            this.CB_Student.Location = new System.Drawing.Point(287, 15);
            this.CB_Student.Margin = new System.Windows.Forms.Padding(4);
            this.CB_Student.Name = "CB_Student";
            this.CB_Student.Size = new System.Drawing.Size(279, 37);
            this.CB_Student.TabIndex = 4;
            this.CB_Student.SelectedIndexChanged += new System.EventHandler(this.CB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.label2.Location = new System.Drawing.Point(627, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Статус обучения";
            // 
            // CB_StatusTraining
            // 
            this.CB_StatusTraining.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CB_StatusTraining.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.CB_StatusTraining.FormattingEnabled = true;
            this.CB_StatusTraining.Location = new System.Drawing.Point(853, 15);
            this.CB_StatusTraining.Margin = new System.Windows.Forms.Padding(4);
            this.CB_StatusTraining.Name = "CB_StatusTraining";
            this.CB_StatusTraining.Size = new System.Drawing.Size(279, 37);
            this.CB_StatusTraining.TabIndex = 5;
            this.CB_StatusTraining.SelectedIndexChanged += new System.EventHandler(this.CB_SelectedIndexChanged);
            // 
            // Table
            // 
            this.Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Table.ColumnCount = 1;
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Table.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.Table.Location = new System.Drawing.Point(0, 76);
            this.Table.Margin = new System.Windows.Forms.Padding(5, 12, 5, 5);
            this.Table.Name = "Table";
            this.Table.RowCount = 1;
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Table.Size = new System.Drawing.Size(1364, 843);
            this.Table.TabIndex = 6;
            // 
            // AdminAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 919);
            this.Controls.Add(this.Table);
            this.Controls.Add(this.CB_StatusTraining);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CB_Student);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminAccount";
            this.ShowIcon = false;
            this.Text = "Учетная запись администратора";
            this.Load += new System.EventHandler(this.AdminAccount_Load);
            //this.Resize += new System.EventHandler(this.AdminAccount_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_Student;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CB_StatusTraining;
        private System.Windows.Forms.TableLayoutPanel Table;
    }
}