namespace Aruco
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
            this.button1 = new System.Windows.Forms.Button();
            this.ipAddress_textBox = new System.Windows.Forms.TextBox();
            this.port_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.login_textBox = new System.Windows.Forms.TextBox();
            this.password_textBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.netCamera_radioButton = new System.Windows.Forms.RadioButton();
            this.usbCam_radioButton = new System.Windows.Forms.RadioButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_spots = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(314, 287);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ipAddress_textBox
            // 
            this.ipAddress_textBox.Location = new System.Drawing.Point(11, 48);
            this.ipAddress_textBox.Name = "ipAddress_textBox";
            this.ipAddress_textBox.Size = new System.Drawing.Size(166, 20);
            this.ipAddress_textBox.TabIndex = 1;
            // 
            // port_textBox
            // 
            this.port_textBox.Location = new System.Drawing.Point(11, 91);
            this.port_textBox.Name = "port_textBox";
            this.port_textBox.Size = new System.Drawing.Size(166, 20);
            this.port_textBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP адрес";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Порт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Логин";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Пароль";
            // 
            // login_textBox
            // 
            this.login_textBox.Location = new System.Drawing.Point(11, 135);
            this.login_textBox.Name = "login_textBox";
            this.login_textBox.Size = new System.Drawing.Size(166, 20);
            this.login_textBox.TabIndex = 7;
            // 
            // password_textBox
            // 
            this.password_textBox.Location = new System.Drawing.Point(11, 174);
            this.password_textBox.Name = "password_textBox";
            this.password_textBox.PasswordChar = '*';
            this.password_textBox.Size = new System.Drawing.Size(166, 20);
            this.password_textBox.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(233, 287);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // netCamera_radioButton
            // 
            this.netCamera_radioButton.AutoSize = true;
            this.netCamera_radioButton.Location = new System.Drawing.Point(12, 12);
            this.netCamera_radioButton.Name = "netCamera_radioButton";
            this.netCamera_radioButton.Size = new System.Drawing.Size(67, 17);
            this.netCamera_radioButton.TabIndex = 10;
            this.netCamera_radioButton.TabStop = true;
            this.netCamera_radioButton.Text = "Сетевая";
            this.netCamera_radioButton.UseVisualStyleBackColor = true;
            this.netCamera_radioButton.CheckedChanged += new System.EventHandler(this.netCamera_radioButton_CheckedChanged);
            // 
            // usbCam_radioButton
            // 
            this.usbCam_radioButton.AutoSize = true;
            this.usbCam_radioButton.Location = new System.Drawing.Point(199, 12);
            this.usbCam_radioButton.Name = "usbCam_radioButton";
            this.usbCam_radioButton.Size = new System.Drawing.Size(47, 17);
            this.usbCam_radioButton.TabIndex = 11;
            this.usbCam_radioButton.TabStop = true;
            this.usbCam_radioButton.Text = "USB";
            this.usbCam_radioButton.UseVisualStyleBackColor = true;
            this.usbCam_radioButton.CheckedChanged += new System.EventHandler(this.usbCam_radioButton_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(183, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(206, 147);
            this.listBox1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Отслеживаемые места";
            // 
            // textBox_spots
            // 
            this.textBox_spots.Location = new System.Drawing.Point(11, 236);
            this.textBox_spots.Name = "textBox_spots";
            this.textBox_spots.Size = new System.Drawing.Size(378, 20);
            this.textBox_spots.TabIndex = 15;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 322);
            this.Controls.Add(this.textBox_spots);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.usbCam_radioButton);
            this.Controls.Add(this.netCamera_radioButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.password_textBox);
            this.Controls.Add(this.login_textBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.port_textBox);
            this.Controls.Add(this.ipAddress_textBox);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox ipAddress_textBox;
        private System.Windows.Forms.TextBox port_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox login_textBox;
        private System.Windows.Forms.TextBox password_textBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton netCamera_radioButton;
        private System.Windows.Forms.RadioButton usbCam_radioButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_spots;
    }
}