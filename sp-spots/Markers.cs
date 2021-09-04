using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aruco
{
    class Markers
    {
        List<int> _observedSpotsId;
        public Markers(int markersX, int markersY, int markersSeparation, int markersLength, List<int> observedSpotsId)
        {
            this.markersX = markersX;
            this.markersY = markersY;
            this.markersSeparation = markersSeparation;
            this.markersLength = markersLength;
            spots = new Spot[observedSpotsId.Count];
            for(int i=0; i<spots.Length; i++)
            {
                spots[i] = new Spot(observedSpotsId[i]);
            }
            _observedSpotsId = observedSpotsId;
        }
        int markersX;
        int markersY;
        int markersSeparation;
        int markersLength;

        private Dictionary _dict;
        Mat _cameraMatrix = new Mat();
        Mat _distCoeffs = new Mat();
        Spot[] spots;
        private Dictionary ArucoDictionary
        {
            get
            {
                if (_dict == null)
                {
                    _dict = new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_100);
                }

                return _dict;
            }
        }

        private GridBoard _gridBoard;
        private GridBoard ArucoBoard
        {
            get
            {
                if (_gridBoard == null)
                {
                    _gridBoard = new GridBoard(markersX, markersY, markersLength, markersSeparation, ArucoDictionary);
                }
                return _gridBoard;
            }
        }

        void refreshSpots(int[] arr)
        {
            for(int i=0; i<spots.Length; i++)
            {
                bool occupied = true;
                for(int j=0; j<arr.Length; j++)
                {
                    if (spots[i].getId() == arr[j])
                    {
                        occupied = false;
                        break;
                    }
                }
                spots[i].Occupied(occupied);
            }
        }

        public Spot[] GetSpots()
        {
            return spots;
        }

        Mat rvecs = new Mat();
        Mat tvecs = new Mat();
        public Mat ProcessFrame(Mat _frame)
        {
            using (VectorOfInt ids = new VectorOfInt())
            using (VectorOfVectorOfPointF corners = new VectorOfVectorOfPointF())
            using (VectorOfVectorOfPointF rejected = new VectorOfVectorOfPointF())
            {
                DetectorParameters p = DetectorParameters.GetDefault();
                ArucoInvoke.DetectMarkers(_frame, ArucoDictionary, corners, ids, p, rejected);
                ArucoInvoke.RefineDetectedMarkers(_frame, ArucoBoard, corners, ids, rejected, null, null, 10, 3, true, null, p);
                //_frame.CopyTo(_frameCopy);

                refreshSpots(ids.ToArray());

                if (ids.Size > 0)
                {
                    ArucoInvoke.DrawDetectedMarkers(_frame, corners, ids, new MCvScalar(0, 255, 0));

                    if (!_cameraMatrix.IsEmpty && !_distCoeffs.IsEmpty)
                    {
                        ArucoInvoke.EstimatePoseSingleMarkers(corners, markersLength, _cameraMatrix, _distCoeffs, rvecs, tvecs);
                        for (int i = 0; i < ids.Size; i++)
                        {
                            using (Mat rvecMat = rvecs.Row(i))
                            using (Mat tvecMat = tvecs.Row(i))
                            using (VectorOfDouble rvec = new VectorOfDouble())
                            using (VectorOfDouble tvec = new VectorOfDouble())
                            {
                                double[] values = new double[3];
                                rvecMat.CopyTo(values);
                                rvec.Push(values);
                                tvecMat.CopyTo(values);
                                tvec.Push(values);
                                ArucoInvoke.DrawAxis(_frame, _cameraMatrix, _distCoeffs, rvec, tvec, markersLength * 0.5f);
                            }
                        }
                    }

                }
                
            }

            return _frame;
        }

        

        public void printArucoBoard()
        {
            Size imageSize = new Size();
            int margins = markersSeparation;
            int w = markersX * (markersLength + markersSeparation) - markersSeparation + 2 * margins;
            int h = markersY * (markersLength + markersSeparation) - markersSeparation + 2 * margins;
            imageSize.Width = Math.Max(w, h);
            imageSize.Height = Math.Max(w, h);
            int borderBits = 1;

            Mat boardImage = new Mat();
            ArucoBoard.Draw(imageSize, boardImage, margins, borderBits);
            bmIm = boardImage.Bitmap;
            PrintImage();
        }

        private void PrintImage()
        {
            PrintDocument pd = new PrintDocument();
            //pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            //pd.OriginAtMargins = false;
            //pd.DefaultPageSettings.Landscape = true;

            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();

            printPreviewDialog1.Document = pd;
            //printPreviewDialog1.AutoScale = true;
            printPreviewDialog1.ShowDialog();
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            double cmToUnits = 100 / 2.54;
            e.Graphics.DrawImage(bmIm, 0, 0, (float)(15 * cmToUnits), (float)(15 * cmToUnits));
        }

        Image bmIm;
    }
}
