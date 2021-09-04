using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using openalprnet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ALPRV9000
{
    class BoxFunctions
    {
        public static Mat Image;
        public static List<CompareResult> compare_results;
        public static bool DoCompareLog;
        public static bool CmpBoxes(RotatedRect box1, RotatedRect box2)
        {
            bool result = false;//Если не равны

            //Левый нижний угол box1
            float box1_x1 = box1.Center.X - box1.Size.Width / 2;
            float box1_y1 = box1.Center.Y - box1.Size.Height / 2;

            //Правый верхний угол box1
            float box1_x2 = box1.Center.X + box1.Size.Width / 2;
            float box1_y2 = box1.Center.Y + box1.Size.Height / 2;

            //Левый нижний угол box2
            float box2_x1 = box2.Center.X - box2.Size.Width / 2;
            float box2_y1 = box2.Center.Y - box2.Size.Height / 2;

            //Правый верхний угол box2
            float box2_x2 = box2.Center.X + box2.Size.Width / 2;
            float box2_y2 = box2.Center.Y + box2.Size.Height / 2;


            float tolerance_x = box1.Size.Width / 7;
            float tolerance_y = box1.Size.Height / 7;
            if (box1_x1 > box2_x1 && box1_x2 < box2_x2)
            {
                if (box1_y1 > box2_y1 && box1_y2 < box2_y2)
                {
                    result = true; //Box1 внутри Box2
                }
            }

            if (box2_x1 > box1_x1 && box2_x2 < box1_x2)
            {
                if (box2_y1 > box1_y1 && box2_y2 < box1_y2)
                {
                    result = true; //Box2 внутри Box1
                }
            }



            if (box1.Center.X < box2.Center.X + tolerance_x && box1.Center.X > box2.Center.X - tolerance_x)
            {
                if (box1.Center.Y < box2.Center.Y + tolerance_y && box1.Center.Y > box2.Center.Y - tolerance_y)
                {
                    if (box1.Size.Height < box2.Size.Height + tolerance_x && box1.Size.Height > box2.Size.Height - tolerance_x)
                    {
                        if (box1.Size.Width < box2.Size.Width + tolerance_y && box1.Size.Width > box2.Size.Width - tolerance_y)
                        {
                            result = true;
                        }
                    }

                }
            }

            if (DoCompareLog)
            {
                string details =
                    "box1 center(" + (int)box1.Center.X + " и " + (int)box1.Center.Y + ") size(" + (int)box1.Size.Width + " и " + (int)box1.Size.Height + ")" + System.Environment.NewLine +
                    "box2 center(" + (int)box2.Center.X + " и " + (int)box2.Center.Y + ") size(" + (int)box2.Size.Width + " и " + (int)box2.Size.Height + ")";
                List<UMat> Umats = ImageFunctions.GetImagesInContours(new List<RotatedRect>(new RotatedRect[] { box1, box2 }), Image);
                CompareResult cr = new CompareResult(Umats[0].Bitmap, Umats[1].Bitmap, result.ToString(), details);
                compare_results.Add(cr);
            }
            return result;
        }

        public static List<RotatedRect> GetBoxesFromContours(VectorOfVectorOfPoint contours, Size minSize, Size maxSize)
        {
            List<RotatedRect> boxes = new List<RotatedRect>();
            for (int i = 0; i < contours.Size; i++)
            {
                RotatedRect box = CvInvoke.MinAreaRect(contours[i]);
                if (box.Size.Height < minSize.Height || box.Size.Width < minSize.Width) continue;
                if (box.Size.Height > maxSize.Height || box.Size.Width > maxSize.Width) continue;
                boxes.Add(box);
            }

            return boxes;
        }

        public static List<int> CheckBoxInArray(List<RotatedRect> boxes, RotatedRect box)
        {
            List<int> indexes_remove = new List<int>();
            for (int i = 0; i < boxes.Count; i++)
            {
                if (BoxFunctions.CmpBoxes(boxes[i], box))
                {

                    indexes_remove.Add(i);

                }

            }
            return indexes_remove;
        }

        public static List<RotatedRect> RemoveRepeatedBoxes(List<RotatedRect> boxes)
        {
            if (compare_results == null)
                compare_results = new List<CompareResult>();
            else
                compare_results.Clear();

            List<RotatedRect> boxes_result = new List<RotatedRect>();
            List<int> indexes_remove = new List<int>();
            for (int i = 0; i < boxes.Count; i++)
            {
                indexes_remove = CheckBoxInArray(boxes_result, boxes[i]);
                if (indexes_remove.Count() == 0)
                    boxes_result.Add(boxes[i]); //Если в массиве нет такого
                else
                {
                    List<RotatedRect> temp = new List<RotatedRect>();
                    for (int z = 0; z < boxes_result.Count; z++)
                    {
                        if (indexes_remove.Contains(z)) continue;
                        temp.Add(boxes_result[z]);
                    }
                    boxes_result = new List<RotatedRect>(temp.ToArray<RotatedRect>());
                    boxes_result.Add(boxes[i]); //Если в массиве нет такого

                }


            }
            return boxes_result;
        }

        public static List<RotatedRect> SortBoxes(List<RotatedRect> boxes)
        {
            List<RotatedRect> sortBoxes = new List<RotatedRect>(boxes);
            for (int i = 0; i < sortBoxes.Count - 2; i++)
            {
                bool exchange = false;
                for (int j = 0; j < sortBoxes.Count - i - 1; j++)
                {

                    if (sortBoxes[j].Center.X > sortBoxes[j + 1].Center.X)
                    {
                        RotatedRect tmp = sortBoxes[j];
                        sortBoxes[j] = sortBoxes[j + 1];
                        sortBoxes[j + 1] = tmp;
                        exchange = true;
                    }
                }
                if (!exchange) break;
            }
            return sortBoxes;
        }

        public static Rectangle BoundingRectangle(List<Point> points)
        {
            // Add checks here, if necessary, to make sure that points is not null,
            // and that it contains at least one (or perhaps two?) elements

            var minX = points.Min(p => p.X);
            var minY = points.Min(p => p.Y);
            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);

            var height = maxY - minY;
            var width = maxX - minX;
            var needWidth = height * 6.5;
            //maxX += (int)(needWidth - width);

            return new Rectangle(new Point(minX, minY), new Size(maxX - minX, maxY - minY));
        }

        public static AlprPlateResultNet GetBestResult(AlprResultsNet results)
        {
            AlprPlateResultNet result = null;
            int S_max = 0, index_max = -1; ;
            for(int i=0; i< results.Plates.Count; i++)
            {
                int h = results.Plates[i].PlatePoints[3].Y - results.Plates[i].PlatePoints[0].Y;
                int w = results.Plates[i].PlatePoints[1].X - results.Plates[i].PlatePoints[0].X;
                int s = h * w;
                if (s > S_max)
                {
                    S_max = s;
                    index_max = i;
                }
               
            }

            if (index_max > -1)
                result = results.Plates[index_max];

            return result;
        }

    }
}
