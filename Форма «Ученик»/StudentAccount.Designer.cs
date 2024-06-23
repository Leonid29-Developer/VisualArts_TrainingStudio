namespace VisualArtsTrainingStudio
{
    partial class StudentAccount
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
            this.Label_Request = new System.Windows.Forms.Label();
            this.Table = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // Label_Request
            // 
            this.Label_Request.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label_Request.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Request.Font = new System.Drawing.Font("Times New Roman", 21.75F);
            this.Label_Request.Location = new System.Drawing.Point(428, 32);
            this.Label_Request.Margin = new System.Windows.Forms.Padding(12);
            this.Label_Request.Name = "Label_Request";
            this.Label_Request.Size = new System.Drawing.Size(243, 43);
            this.Label_Request.TabIndex = 2;
            this.Label_Request.Text = "Записаться еще";
            this.Label_Request.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Request.Click += new System.EventHandler(this.Label_Request_Click);
            // 
            // Table
            // 
            this.Table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Table.ColumnCount = 1;
            this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Table.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.Table.Location = new System.Drawing.Point(0, 100);
            this.Table.Margin = new System.Windows.Forms.Padding(4);
            this.Table.Name = "Table";
            this.Table.RowCount = 1;
            this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table.Size = new System.Drawing.Size(1025, 647);
            this.Table.TabIndex = 0;
            // 
            // StudentAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 747);
            this.Controls.Add(this.Table);
            this.Controls.Add(this.Label_Request);
            this.Name = "StudentAccount";
            this.ShowIcon = false;
            this.Text = "Учетная запись ученика";
            this.Load += new System.EventHandler(this.StudentAccount_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label_Request;
        private System.Windows.Forms.TableLayoutPanel Table;
    }
}