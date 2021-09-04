using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ALPRV9000
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            Load();
            ipAddress_textBox.Text = _ipAddress;
            port_textBox.Text = _port.ToString();
            login_textBox.Text = _login;
            password_textBox.Text = _password;
            if (_netCamera)
            {
                netCamera_radioButton.Checked = true;
            }
            else
            {
                usbCam_radioButton.Checked = true;
            }
        }

        private static string _ipAddress;
        private static int _port;
        private static string _login;
        private static string _password;
        private static bool _netCamera;

        public static string IP_Address
        {
            get { return _ipAddress; }
        }
        public static int Port
        {
            get { return _port; }
        }

        public static string Login
        {
            get { return _login; }
        }

        public static string Password
        {
            get { return _password; }
        }

        public static bool ThisIsNetCamera
        {
            get { return _netCamera; }
        }

        public static new void Load()
        {
            if (File.Exists("settings.conf"))
            {
                try
                {
                    string[] settings = File.ReadAllLines("settings.conf");
                    _ipAddress = settings[0];
                    _port = int.Parse(settings[1]);
                    _login = settings[2];
                    _password = settings[3];
                    _netCamera = bool.Parse(settings[4]);
                }
                catch
                {
                    _ipAddress = "192.168.1.100";
                    _port = 554;
                    _login = "user";
                    _password = "123";
                    _netCamera = true;
                }


            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress address = IPAddress.Parse(ipAddress_textBox.Text);
                _ipAddress = address.ToString();

            }
            catch (Exception ex)
            {
                _ipAddress = "192.168.1.100";
                MessageBox.Show(ex.Message);
                return;
            }

            try
            {
                int port = int.Parse(port_textBox.Text);
                _port = port;
            }
            catch (Exception ex)
            {
                _port = 1024;
                MessageBox.Show(ex.Message);
                return;
            }

            _login = login_textBox.Text;
            _password = password_textBox.Text;

            _netCamera = netCamera_radioButton.Checked;

            string[] settings = new string[5];
            settings[0] = _ipAddress;
            settings[1] = _port.ToString();
            settings[2] = _login;
            settings[3] = _password;
            settings[4] = _netCamera.ToString();
            File.WriteAllLines("settings.conf", settings);

            this.Close();
        }
    }
}
