namespace Aruco
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.printArucoBoardButton = new System.Windows.Forms.Button();
            this.startCameraButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.stopCameraButton = new System.Windows.Forms.Button();
            this.cameraImageBox = new Emgu.CV.UI.ImageBox();
            this.messageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cameraImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // printArucoBoardButton
            // 
            this.printArucoBoardButton.Location = new System.Drawing.Point(9, 8);
            this.printArucoBoardButton.Margin = new System.Windows.Forms.Padding(2);
            this.printArucoBoardButton.Name = "printArucoBoardButton";
            this.printArucoBoardButton.Size = new System.Drawing.Size(110, 27);
            this.printArucoBoardButton.TabIndex = 0;
            this.printArucoBoardButton.Text = "Print Aruco Board";
            this.printArucoBoardButton.UseVisualStyleBackColor = true;
            this.printArucoBoardButton.Click += new System.EventHandler(this.printArucoBoardButton_Click);
            // 
            // startCameraButton
            // 
            this.startCameraButton.Location = new System.Drawing.Point(282, 12);
            this.startCameraButton.Margin = new System.Windows.Forms.Padding(2);
            this.startCameraButton.Name = "startCameraButton";
            this.startCameraButton.Size = new System.Drawing.Size(77, 23);
            this.startCameraButton.TabIndex = 1;
            this.startCameraButton.Text = "Старт";
            this.startCameraButton.UseVisualStyleBackColor = true;
            this.startCameraButton.Click += new System.EventHandler(this.cameraButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(581, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Настройки";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonSettings);
            // 
            // stopCameraButton
            // 
            this.stopCameraButton.Location = new System.Drawing.Point(364, 12);
            this.stopCameraButton.Name = "stopCameraButton";
            this.stopCameraButton.Size = new System.Drawing.Size(75, 23);
            this.stopCameraButton.TabIndex = 7;
            this.stopCameraButton.Text = "Стоп";
            this.stopCameraButton.UseVisualStyleBackColor = true;
            this.stopCameraButton.Click += new System.EventHandler(this.stopCameraButton_Click);
            // 
            // cameraImageBox
            // 
            this.cameraImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraImageBox.Location = new System.Drawing.Point(9, 40);
            this.cameraImageBox.Margin = new System.Windows.Forms.Padding(2);
            this.cameraImageBox.Name = "cameraImageBox";
            this.cameraImageBox.Size = new System.Drawing.Size(929, 499);
            this.cameraImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cameraImageBox.TabIndex = 8;
            this.cameraImageBox.TabStop = false;
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(13, 581);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(37, 13);
            this.messageLabel.TabIndex = 9;
            this.messageLabel.Text = "Текст";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 606);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.cameraImageBox);
            this.Controls.Add(this.stopCameraButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.startCameraButton);
            this.Controls.Add(this.printArucoBoardButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Spots Monitoring";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.cameraImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button printArucoBoardButton;
        private System.Windows.Forms.Button startCameraButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button stopCameraButton;
        private Emgu.CV.UI.ImageBox cameraImageBox;
        private System.Windows.Forms.Label messageLabel;
    }
}

