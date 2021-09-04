using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aruco
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
            LoadConfig();
            ipAddress_textBox.Text = _ipAddress;
            port_textBox.Text = _port.ToString();
            login_textBox.Text = _login;
            password_textBox.Text = _password;

            foreach(var spot in _spots)
            {
                textBox_spots.Text += spot.ToString() + ";";
            }
            

            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            listBox1.Items.Clear();
            for (int i=0; i<videoDevices.Count; i++)
            {                
                listBox1.Items.Add(videoDevices[i].Name);
            }
            if (_usbCamIndex >= listBox1.Items.Count) _usbCamIndex = 0;
            listBox1.SetSelected(_usbCamIndex, true);
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
        private static int _usbCamIndex;
        private static List<int> _spots;
        public static int UsbCamIndex
        {
            get { return _usbCamIndex; }
        }

        public static List<int> SPOTS
        {
            get { return _spots; }
        }

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

        public static void LoadConfig()
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
                    _usbCamIndex = int.Parse(settings[5]);
                    setSpots(settings[6]);
                }
                catch (Exception ex)
                {
                    _ipAddress = "192.168.1.100";
                    _port = 554;
                    _login = "user";
                    _password = "123";
                    _netCamera = true;
                }

               
            }
        }

        static void setSpots(string str)
        {
            string buf = "";
            _spots = new List<int>();
            if (str[str.Length - 1] != ';')
            {
                str += ";";
            }
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != ';')
                {
                    buf += str[i];
                }
                else
                {
                    _spots.Add(int.Parse(buf));
                    buf = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

            try
            {
                setSpots(textBox_spots.Text);
            }
            catch
            {
                MessageBox.Show("Места заданы не правильно");
            }

            _login = login_textBox.Text;
            _password = password_textBox.Text;

            _netCamera = netCamera_radioButton.Checked;
            _usbCamIndex = listBox1.SelectedIndex;
            string[] settings = new string[7];
            settings[0] = _ipAddress;
            settings[1] = _port.ToString();
            settings[2] = _login;
            settings[3] = _password;
            settings[4] = _netCamera.ToString();
            settings[5] = listBox1.SelectedIndex.ToString();
            settings[6] = textBox_spots.Text;
            File.WriteAllLines("settings.conf", settings);

            this.Close();

        }

        private void usbCam_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(usbCam_radioButton.Checked)
            {
                ipAddress_textBox.Enabled = false;
                port_textBox.Enabled = false;
                login_textBox.Enabled = false;
                password_textBox.Enabled = false;
            }
        }

        private void netCamera_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (netCamera_radioButton.Checked)
            {
                ipAddress_textBox.Enabled = true;
                port_textBox.Enabled = true;
                login_textBox.Enabled = true;
                password_textBox.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
