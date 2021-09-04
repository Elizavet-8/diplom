using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Aruco
{
    class MyCamera
    {
        Mat _frame = new Mat();
        Mat _frameCopy = new Mat();
        private VideoCapture _capture = null;
        private bool _captureInProgress;
        bool netCamera = true;
        public MyCamera(int index)
        {
            netCamera = false;
            _capture = new VideoCapture(index);

            _capture.ImageGrabbed += ProcessFrame;
        }
        string connection_string;

        void tryConnect()
        {
            _capture = new VideoCapture(connection_string);//Сетевая камера 
            _capture.ImageGrabbed += ProcessFrame;
        }
        Thread thread1;
        public MyCamera(string ip, int port, string login, string password)
        {
            netCamera = true;
            thread1 = new Thread(new ThreadStart(tryConnect));
            thread1.IsBackground = true;
            thread1.Name = "TryConnectThread";
            thread1.Start();
            string connection_string = "rtsp://" + login + ":" + password + "@" + ip + "/StreamingSetting?version=1.0&action=getRTSPStream&ChannelID=1&ChannelName=Channel1";
            this.connection_string = connection_string;   
        }
        public delegate void GetFrame(Mat bitMap);
        GetFrame getFrame;
        public void setCallBack(GetFrame getFrame)
        {
            this.getFrame = getFrame;
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            if (_capture != null && _capture.Ptr != IntPtr.Zero)
            {
                _capture.Retrieve(_frame, 0);
                getFrame(_frame);              
            }
        }


        bool check(ProcessThreadCollection tc, ProcessThread t)
        {
            bool result = false;
            for (int i = 0; i < tc.Count; i++)
            {
                if (tc[i].Id == t.Id)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void Start()
        {
            if(netCamera)
                thread1.Join(5000);
            try
            {
                _capture.Start();
                _captureInProgress = true;
            }
            catch
            {
                throw new TimeoutException("Не удалось подключиться к камере");
            }           
            
        }

        public void Stop()
        {
            if (_capture != null)
            {
                _captureInProgress = false;
                _capture.Pause();
            }
        }

        public bool getCaptureProgress()
        {
            return _captureInProgress;
        }

        public void Pause()
        {
            _captureInProgress = false;
            _capture.Pause();
        }
    }
}
