//----------------------------------------------------------------------------
//  Copyright (C) 2004-2016 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading;

namespace Aruco
{
   public partial class Form1 : Form
   {
        public delegate void SendData(int[] arr);
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;


        public Form1()
        {
            _client = new MongoClient();
            FormSettings.LoadConfig();
            markers = new Markers(2, 5, 30, 80, FormSettings.SPOTS);
            _database = _client.GetDatabase("sp_db");
            InitializeComponent();
            
        }

        Markers markers;
        Spot[] spots;

        delegate void GetFrame(Mat bitMap);
        public void getFrame(Mat frame)
        {
            GetFrame del = getFrame;
            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(del, frame);
                }
                catch
                { }

            }
            else
            {
                cameraImageBox.Image = markers.ProcessFrame(frame);
                spots = markers.GetSpots();
            }
           
        }

        MyCamera myCam;
        private void cameraButton_Click(object sender, EventArgs e)
        {
            startCameraButton.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            if (FormSettings.ThisIsNetCamera)
            {
                myCam = new MyCamera(FormSettings.IP_Address, FormSettings.Port, FormSettings.Login, FormSettings.Password);
            }
            else
                myCam = new MyCamera(FormSettings.UsbCamIndex);

            MyCamera.GetFrame gf = getFrame;
            myCam.setCallBack(gf);
            try
            {
                myCam.Start();
                timer1.Start();
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

        delegate void VoidFunction();   

        private void button1_Click(object sender, EventArgs e)
        {
            FormSettings FS = new FormSettings();
            FS.ShowDialog();
        }

        private void printArucoBoardButton_Click(object sender, EventArgs e)
        {
            markers.printArucoBoard();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (spots == null)
                return;

            for(int i=0;i< spots.Length; i++)
            {
                //messageLabel.Text = "Ошибок " + errors.ToString();
                var collection = _database.GetCollection<BsonDocument>("parking");
                var filter = Builders<BsonDocument>.Filter.Eq("name", "A"+spots[i].getId());
                
                var update = Builders<BsonDocument>.Update.Set("occupiedByVideo", spots[i].IsOccupied());
                try
                {
                    var result = await collection.UpdateOneAsync(filter, update);
                }
                catch(Exception ex)
                {
                    messageLabel.Text = "Не удалось обновить БД " + ex.Message;
                }
               
            }           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //stop the capture
            timer1.Stop();
            startCameraButton.Text = "Start Capture";
            if(myCam != null)
                myCam.Stop();
 
        }

        private void stopCameraButton_Click(object sender, EventArgs e)
        {
            stopCameraButton.Enabled = false;
            startCameraButton.Enabled = true;
            timer1.Stop();
            myCam.Stop();
        }

        private void buttonSettings(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }
    }
}
