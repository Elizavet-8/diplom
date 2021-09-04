using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using openalprnet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace ALPRV9000
{
    class NumberRecognizer
    {
        public NumberRecognizer()
        {
            isWorking = false;
        }

        public delegate void RetrieveNumbers(List<NumberResult> numberResultsTrue, List<NumberResult> numberResultsFalse);
        public event RetrieveNumbers NumberRecognize; 
        private List<Bitmap> images_steps = new List<Bitmap>();
        public List<Bitmap> getStepsImages()
        { return images_steps; } 

        private void AddUmatToLog(List<UMat> images)
        {
            foreach (var image in images)
                images_steps.Add(image.Clone().Bitmap);
        }

        private void AddBitmapToLog(Bitmap image)
        { images_steps.Add(image); }

        private void AddBitmapsToLog(List<Bitmap> images)
        { images_steps.AddRange(images); }

        
        bool isWorking;

        public bool IsWorking
        {
            get { return isWorking; }
        }

        void threadTask(Object stateInfo)
        {
            Bitmap image = (Bitmap)stateInfo;
            Console.WriteLine("Начинаю распознавание");
            images_steps.Clear();
            Bitmap numberPlate = findNumberPlate(image);
            if (numberPlate != null)
                Recognize(numberPlate);
            numberPlate = null;
            NumberRecognize(numberResultsTrue, numberResultsFalse);
            isWorking = false;
            Console.WriteLine("Закончил распознавание");
        }

        public void getNumberCar(Bitmap image)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(threadTask), image);
            isWorking = true;         
                   
        }

        //Mat filteredGray_global;
        private void Recognize(Image imageSource)
        {
            string number = "";
            MyTesseract myTesseract = new MyTesseract();
            using (Bitmap bitMap = new Bitmap(imageSource))
            //using (Bitmap ExtendedBitMap = ImageFunctions.ExtendBitmap(bitMap, new Size(bitMap.Width, bitMap.Height + 20), new Point(0, 10), Brushes.Tomato))
            using (Mat imageMat = ImageFunctions.BitMapToMat(bitMap))//В нужный формат
            using (Mat gray = new Mat()) //Серое
            {
                CvInvoke.CvtColor(imageMat, gray, ColorConversion.Bgr2Gray);
                using (Mat grayResized = ImageFunctions.Resize(gray, new Size(600, 150))) //Изменен размер
                using (Mat filteredGray = ImageFunctions.FilterPlate(grayResized))//Контуры
                using (Mat canny = new Mat())//Контуры
                {
                    //filteredGray_global = filteredGray;//для отладки
                    CvInvoke.Canny(filteredGray, canny, 50, 100, 3, false);
                    using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())//Контуры                    
                    using (Mat hierarchy = new Mat())//Какая то фигня
                    {
                        BoxFunctions.Image = filteredGray;
                        CvInvoke.FindContours(canny, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                        //int[,] hierachy = CvInvoke.FindContourTree(canny, contours, ChainApproxMethod.ChainApproxSimple);
                        List<RotatedRect> boxes = BoxFunctions.GetBoxesFromContours(contours, new Size(20, 20), new Size(80, 100)); //Которбки не меньше указанного размера
                        //List<UMat> boxes_sizeFiltered = getNormImagesInContours(boxes, grayResized); //для отладки
                        List<RotatedRect> boxes_smartFiltered = BoxFunctions.RemoveRepeatedBoxes(boxes);//Убираем повторения
                        List<RotatedRect> boxes_sort = BoxFunctions.SortBoxes(boxes_smartFiltered);//Сортируем в правильном порядке
                        List<UMat> plates = ImageFunctions.GetImagesInContours(boxes_sort, filteredGray); //Картинки из контуров
                        List<UMat> filteredPlates = ImageFunctions.FilterPlates(plates); //Отфильтрованные   
                        List<UMat> resizedFilteredPlates = ImageFunctions.Resize(filteredPlates, new Size(240, 180)); //меняем размер 
                        List<Bitmap> segments = ImageFunctions.UmatsToBitmaps(resizedFilteredPlates);
                        List<Bitmap> extendedContours = ImageFunctions.ExtendBitmaps(segments, 40, 40, new Point(20, 20), Brushes.Black);
                        number = myTesseract.GetNumberFromContours(extendedContours);
                        //Mat testtest = FilterPlateMat(filteredGray);
                        //number2 = getNumberFromPlate(testtest);
                        AddBitmapToLog(grayResized.Clone().Bitmap);
                        //AddBitmapToLog((Bitmap)filteredGray.Bitmap.Clone());
                        //AddBitmapToLog(canny.Clone().Bitmap);
                        //AddUmatToLog(filteredPlates);
                        //AddUmatToLog(boxes_sizeFiltered);
                        AddBitmapsToLog(extendedContours);
                        myTesseract = null;
                        boxes = null;
                        boxes_smartFiltered = null;
                        boxes_sort = null;
                        plates = null;
                        filteredPlates = null;
                        resizedFilteredPlates = null;
                    }
                }
            }

            
            string numberTrue = NumberNormalize.getNormalizeNumber(number);
            if (numberTrue.Length == 6)
                numberResultsTrue.Add(new NumberResult(numberTrue, false));
            else
                numberResultsFalse.Add(new NumberResult(number, false));
            Console.WriteLine("Вышел из Recognize");

        }
         
        List<NumberResult> numberResultsTrue = new List<NumberResult>();
        List<NumberResult> numberResultsFalse = new List<NumberResult>();
        private Bitmap findNumberPlate(Bitmap image)
        {
            Bitmap numberPlate;
            numberResultsTrue.Clear();
            numberResultsFalse.Clear();
            string config_file = "openalpr.conf";
            string runtime_data_dir = "runtime_data";
            using (var alpr = new AlprNet("eu", config_file, runtime_data_dir))
            {
                if (!alpr.IsLoaded())
                {
                    throw(new Exception("Error initializing OpenALPR"));                    
                }

                alpr.DefaultRegion = "ru";                

                AlprResultsNet results = alpr.Recognize(image);

                AlprPlateResultNet result = BoxFunctions.GetBestResult(results);
                if (result == null) return null;
                foreach(var topNPlates in result.TopNPlates)
                {                        
                    string numb = NumberNormalize.getNormalizeNumber(topNPlates.Characters);
                    if (numb.Length == 6)
                        numberResultsTrue.Add(new NumberResult(numb, true));
                    else
                        numberResultsFalse.Add(new NumberResult(topNPlates.Characters, true)); 

                }        
                List<Point> points = result.PlatePoints;
                List<Point> newPoints = new List<Point>();
                newPoints.Add(new Point(points[0].X, points[0].Y - 15));
                newPoints.Add(new Point(points[1].X, points[1].Y - 15));
                newPoints.Add(new Point(points[2].X, points[2].Y + 15));
                newPoints.Add(new Point(points[3].X, points[3].Y + 15));

                Rectangle rect = BoxFunctions.BoundingRectangle(newPoints);
                points = null;
                using (Bitmap cropped = ImageFunctions.CropImage(image, rect))
                    numberPlate = new Bitmap(cropped);                               
                
                Console.WriteLine("result plates counts " + results.Plates.Count);
                results = null;
                result = null;   
            }      
            
            return numberPlate;
        }  
    }
}
