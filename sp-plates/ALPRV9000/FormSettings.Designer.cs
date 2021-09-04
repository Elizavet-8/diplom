namespace ALPRV9000
{
    partial class FormSettings
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
            this.ipAddress_textBox = new System.Windows.Forms.TextBox();
            this.port_textBox = new System.Windows.Forms.TextBox();
            this.login_textBox = new System.Windows.Forms.TextBox();
            this.password_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.netCamera_radioButton = new System.Windows.Forms.RadioButton();
            this.usbCam_radioButton = new System.Windows.Forms.RadioButton();
            this.cancel_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // ipAddress_textBox
            // 
            this.ipAddress_textBox.Location = new System.Drawing.Point(13, 30);
            this.ipAddress_textBox.Name = "ipAddress_textBox";
            this.ipAddress_textBox.Size = new System.Drawing.Size(217, 20);
            this.ipAddress_textBox.TabIndex = 1;
            // 
            // port_textBox
            // 
            this.port_textBox.Location = new System.Drawing.Point(13, 69);
            this.port_textBox.Name = "port_textBox";
            this.port_textBox.Size = new System.Drawing.Size(217, 20);
            this.port_textBox.TabIndex = 2;
            // 
            // login_textBox
            // 
            this.login_textBox.Location = new System.Drawing.Point(13, 108);
            this.login_textBox.Name = "login_textBox";
            this.login_textBox.Size = new System.Drawing.Size(217, 20);
            this.login_textBox.TabIndex = 3;
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(12, 147);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.Size = new System.Drawing.Size(218, 20);
            this.password_textBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Порт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Логин";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Пароль";
            // 
            // netCamera_radioButton
            // 
            this.netCamera_radioButton.AutoSize = true;
            this.netCamera_radioButton.Location = new System.Drawing.Point(12, 174);
            this.netCamera_radioButton.Name = "netCamera_radioButton";
            this.netCamera_radioButton.Size = new System.Drawing.Size(67, 17);
            this.netCamera_radioButton.TabIndex = 8;
            this.netCamera_radioButton.TabStop = true;
            this.netCamera_radioButton.Text = "Сетевая";
            this.netCamera_radioButton.UseVisualStyleBackColor = true;
            // 
            // usbCam_radioButton
            // 
            this.usbCam_radioButton.AutoSize = true;
            this.usbCam_radioButton.Checked = true;
            this.usbCam_radioButton.Location = new System.Drawing.Point(86, 174);
            this.usbCam_radioButton.Name = "usbCam_radioButton";
            this.usbCam_radioButton.Size = new System.Drawing.Size(47, 17);
            this.usbCam_radioButton.TabIndex = 9;
            this.usbCam_radioButton.TabStop = true;
            this.usbCam_radioButton.Text = "USB";
            this.usbCam_radioButton.UseVisualStyleBackColor = true;
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(12, 226);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 10;
            this.cancel_button.Text = "Отмена";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(94, 226);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 11;
            this.save_button.Text = "Сохранить";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.usbCam_radioButton);
            this.Controls.Add(this.netCamera_radioButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.password_textBox);
            this.Controls.Add(this.login_textBox);
            this.Controls.Add(this.port_textBox);
            this.Controls.Add(this.ipAddress_textBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipAddress_textBox;
        private System.Windows.Forms.TextBox port_textBox;
        private System.Windows.Forms.TextBox login_textBox;
        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton netCamera_radioButton;
        private System.Windows.Forms.RadioButton usbCam_radioButton;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button save_button;
    }
}