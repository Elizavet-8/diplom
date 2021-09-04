using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace ALPRV9000
{
    public partial class Form1 : Form
    {
        NumberRecognizer numberRecognizer;
        public Form1()
        {
            InitializeComponent();
            FormSettings.Load();
            numbers = new List<string>();
            numberRecognizer = new NumberRecognizer();
            numberRecognizer.NumberRecognize += doSomethingWithNumbers;
            //sendNumber("");
        }
       
        private void buttonOpenFile_Click(object sender, EventArgs e)
        {        
            OpenFileDialog of = new OpenFileDialog();
            if(of.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitMap = new Bitmap(of.FileName);
                picOriginal.Image = bitMap;
                clearNumbers();
                startTime = DateTime.Now;
                BoxFunctions.DoCompareLog = true;
                numberRecognizer.getNumberCar(bitMap);
                    
            }       
        }

        string getMostPopular(string[] arr)
        {
            string popular = "";
            int[] entries = new int[arr.Length];
            for(int i=0;i<arr.Length;i++)
            {
                for(int j=0; j<arr.Length; j++)
                {
                    if (arr[i] == arr[j])
                        entries[i]++;
                }
            }

            int index = -1;
            int max = -1;
            if (entries.Length>0)
                max = entries.Max();
            for (int i=0;i<entries.Length; i++)
            {
                if (entries[i] == max)
                {
                    index = i;
                    break;
                }
                    
            }            
           
            if(index>-1)
                popular = arr[index];
            return popular;
        }

        TimeSpan interval = new TimeSpan(0, 0, 30);//30 секунд
        DateTime lastSendTime;
        bool sendNumber(string number)
        {
            if (DateTime.Now - lastSendTime < interval) return false; //Посылаем номер один раз в заданный интервал
            Console.WriteLine("Пытаюсь отправить номер");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:8080/api/parking/auto");
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";

            string json = "{\"plateNumber\":\"" + number + "\"}";

            // turn our request string into a byte stream
            byte[] postBytes = Encoding.UTF8.GetBytes(json);

            // this is important - make sure you specify type this way
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = postBytes.Length;
            //request.CookieContainer = Cookies;
            //request.UserAgent = currentUserAgent;
            
            try
            {
                Stream requestStream = request.GetRequestStream();
                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
            }
           catch(Exception ex)
            {
                printMessage(ex.Message, label_message);
                return false;
            }
            
            lastSendTime = DateTime.Now;
            // grab te response and print it out to the console along with the status code
            request.Timeout = 1000;
            string result = "";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    result = rdr.ReadToEnd();
                }
                Console.WriteLine("Отправил номер");
            }
            catch (Exception ex)
            {
                printMessage(ex.Message, label_message);
                return false;
            }

            return true;

        }

        delegate void ShowNumberDelegate(List<NumberResult> numbers, ListBox listBox);
        void showNumbers(List<NumberResult> numbers, ListBox listBox)
        {
           
            if(this.InvokeRequired)
            {            
                try
                {
                    this.Invoke(new ShowNumberDelegate(showNumbers), new object[] { numbers, listBox });

                }                    
                catch

                {
                    //Случается если закрыть форму
                }
            }
            else
            {
                Console.WriteLine("Отображаю номера");
                foreach(var number in numbers)
                {
                    string str = number.NUMBER;
                    if (!number.ALPR_RESULT)
                        str += "                           *";
                    listBox.Items.Add(str);
                }
                
                labelPopNumber.Text = "Вероятно номер " + getMostPopular(this.numbers.ToArray());
            }            
        }

        delegate void VoidDelegateNoArgs();
        void clearNumbers()
        {

            if (this.InvokeRequired)
            {
                this.Invoke(new VoidDelegateNoArgs(clearNumbers));
            }
            else
            {
                numbers.Clear();
                lbxPlates_true.Items.Clear();
                listBox_false.Items.Clear();
            }
        }

        delegate void PrintMessage(string message, Label label);
        void printMessage(string message, Label label)
        {
            if (label.InvokeRequired)
            {
                this.Invoke(new PrintMessage(printMessage), message, label);
            }
            else
            {
                label.Text = message;
            }
            
        }

        List<string> numbers;
        void doSomethingWithNumbers(List<NumberResult> lastNumbersTrue, List<NumberResult> lastNumbersFalse)
        {            
            Console.WriteLine("Делаю что то с номером");
            TimeSpan recognizeTime = DateTime.Now - startTime; //Время затраченное на распознавание
            int ms = (int)recognizeTime.TotalMilliseconds;
            printMessage(ms.ToString() + " мс", labelTime);

            foreach (var numbResult in lastNumbersTrue)
            {
                numbers.Add(numbResult.NUMBER);
            }

            
            showNumbers(lastNumbersTrue, lbxPlates_true);
            showNumbers(lastNumbersFalse, listBox_false);

            showSteps();//для дебага
            if(numbers.Count>10)  //после накопления как минимум 10 номеров посылаем самый часто встречающийся      
            {
                string popNumber = getMostPopular(numbers.ToArray());                
                if(sendNumber(popNumber))
                    printMessage("Последний отправленный номер " + popNumber, label_message);
                
                clearNumbers();
            } 
                
            
        }

        delegate void ShowStepsDelegate();
        void showSteps()//для дебага
        {     
            if(this.InvokeRequired)
            {
                this.Invoke(new ShowStepsDelegate(showSteps));
            }
            else
            {
                Console.WriteLine("Отображаю шаги");
                panel1.Controls.Clear();
                int offset_x = 0;
                List<Bitmap> steps = numberRecognizer.getStepsImages();
                foreach (var step in steps)
                {
                    PictureBox picBox = new PictureBox();
                    picBox.Size = new Size(step.Width, step.Height);
                    picBox.Location = new Point(offset_x, 0);
                    picBox.Image = step;
                    panel1.Controls.Add(picBox);
                    offset_x += step.Width + 10;
                }

                
                int offset_y = 0;
                panel2.Controls.Clear();
                if(BoxFunctions.DoCompareLog && BoxFunctions.compare_results != null)
                    foreach (var cr in BoxFunctions.compare_results)
                    {
                        PictureBox picBox = new PictureBox();
                        picBox.Size = new Size(cr.img1.Width, cr.img1.Height);
                        picBox.Location = new Point(0, offset_y);
                        picBox.Image = cr.img1;
                        panel2.Controls.Add(picBox);

                        PictureBox picBox2 = new PictureBox();
                        picBox2.Size = new Size(cr.img2.Width, cr.img2.Height);
                        picBox2.Location = new Point(cr.img1.Width, offset_y);
                        picBox2.Image = cr.img2;
                        panel2.Controls.Add(picBox2);
                        offset_y += Math.Max(picBox.Height, picBox2.Height);
                        panel2.Controls.Add(picBox2);

                        Label lab = new Label();
                        lab.Text = cr.result;
                        lab.Location = new Point(picBox2.Width + picBox2.Location.X, picBox2.Location.Y);
                        panel2.Controls.Add(lab);
                        offset_y += 5;

                        RichTextBox rtb = new RichTextBox();
                        rtb.Text = cr.details;
                        rtb.Location = new Point(0, offset_y);
                        rtb.Size = new Size(panel2.Width, 40);
                        offset_y += rtb.Height+20;
                        panel2.Controls.Add(rtb);
                    }
                
            }

        }

        private void button_settings_Click(object sender, EventArgs e)
        {
            FormSettings fs = new FormSettings();
            fs.ShowDialog();
        }

        delegate void GetFrame(Bitmap bitMap);
        public void getFrame(Bitmap bitMap)
        {            
            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(new GetFrame(getFrame), bitMap);
                }
                catch(ObjectDisposedException ex)
                {
                    //Случается если во время видео закрыть форму
                }
                
            }
            else
            {
                
                picOriginal.Image = bitMap;
                if(!numberRecognizer.IsWorking)
                {
                    Console.WriteLine("Отправляю кадр в рекогнайзер");
                    startTime = DateTime.Now;
                    numberRecognizer.getNumberCar(bitMap);                    
                }
                    
            }           
           
        }
        DateTime startTime; //Время когда началось распознавание

        MyCamera myCam;
        private void startCamerabutton_Click(object sender, EventArgs e)
        {
            startCameraButton.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            if (FormSettings.ThisIsNetCamera)
            {                         
                myCam = new MyCamera(FormSettings.IP_Address, FormSettings.Port, FormSettings.Login, FormSettings.Password);    
            }                
            else
                myCam = new MyCamera();

            myCam.ReadyFrame += getFrame;
            try
            {
                BoxFunctions.DoCompareLog = false;
                myCam.Start();
                stopCameraButton.Enabled = true;
            }    
            catch (Exception ex)
            {
                stopCameraButton.Enabled = false;
                startCameraButton.Enabled = true;              
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
                    
           
            
        }

        private void stopCameraButton_Click(object sender, EventArgs e)
        {
            stopCameraButton.Enabled = false;
            startCameraButton.Enabled = true;
            myCam.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {    
            if(myCam!=null)        
                myCam.Stop();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
