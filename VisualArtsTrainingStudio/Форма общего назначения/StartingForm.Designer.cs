
namespace VisualArtsTrainingStudio
{
    partial class StartingForm
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
            this.Label_Authorization = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label_Request
            // 
            this.Label_Request.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label_Request.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Request.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.Label_Request.Location = new System.Drawing.Point(130, 49);
            this.Label_Request.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label_Request.Name = "Label_Request";
            this.Label_Request.Size = new System.Drawing.Size(200, 50);
            this.Label_Request.TabIndex = 0;
            this.Label_Request.Text = "Записаться";
            this.Label_Request.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Request.Click += new System.EventHandler(this.Request_Click);
            // 
            // Label_Authorization
            // 
            this.Label_Authorization.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label_Authorization.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Authorization.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.Label_Authorization.Location = new System.Drawing.Point(130, 135);
            this.Label_Authorization.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Label_Authorization.Name = "Label_Authorization";
            this.Label_Authorization.Size = new System.Drawing.Size(200, 50);
            this.Label_Authorization.TabIndex = 2;
            this.Label_Authorization.Text = "Авторизоваться";
            this.Label_Authorization.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Authorization.Click += new System.EventHandler(this.Authorization_Click);
            // 
            // StartingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 237);
            this.Controls.Add(this.Label_Authorization);
            this.Controls.Add(this.Label_Request);
            this.Name = "StartingForm";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label_Request;
        private System.Windows.Forms.Label Label_Authorization;
    }
}