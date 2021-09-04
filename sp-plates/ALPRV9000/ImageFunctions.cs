using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ALPRV9000
{
    class ImageFunctions
    {
        public static List<Bitmap> ExtendBitmaps(List<Bitmap> srcBitmaps, int addWidth, int addHeight, Point point, Brush color)
        {
            List<Bitmap> extendedBitmaps = new List<Bitmap>();
            foreach (var bitmap in srcBitmaps)
            {
                extendedBitmaps.Add(ExtendBitmap(
                    new Bitmap(bitmap), new Size(bitmap.Width + addWidth, bitmap.Height + addHeight), new Point(addWidth / 2, addHeight / 2), color));
            }
            return extendedBitmaps;
        }
        public static Bitmap ExtendBitmap(Bitmap srcBitmap, Size newSize, Point point, Brush color)
        {
            Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, srcBitmap.PixelFormat);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                // fill target image with white color
                g.FillRectangle(color, 0, 0, newSize.Width, newSize.Height);
                // place source image inside the target image
                g.DrawImage(srcBitmap, point.X, point.Y);
            }
            return newImage;
        }

        public static Mat ExtendMat(Mat srcMat, Size newSize, Point point, Brush color)
        {
            Bitmap tmp = new Bitmap(srcMat.Bitmap);
            Bitmap bitMap = ExtendBitmap(tmp, newSize, point, color);
            //tmp = null;
            Mat extendedMat = BitMapToMat(bitMap);
            return extendedMat;
        }

        public static Mat BitMapToMat(Bitmap bgr_image)
        {
            Image<Bgr, byte> img1 = new Image<Bgr, byte>(bgr_image);
            return img1.Mat;
        }

        public static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            try
            {
                return img.Clone(cropArea, img.PixelFormat);
            }
            catch (OutOfMemoryException ex)
            {
                return (Bitmap)img.Clone();
            }
        }

        public static Mat FilterPlate(Mat plate)
        {
            Mat thresh = new Mat();
            CvInvoke.Threshold(plate, thresh, 100, 255, ThresholdType.Otsu);
            CvInvoke.Erode(thresh, thresh, null, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);
            CvInvoke.Dilate(thresh, thresh, null, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);

            return thresh;
        }

        public static List<UMat> FilterPlates(List<UMat> plates)
        {
            List<UMat> threshes = new List<UMat>();
            foreach (var plate in plates)
            {
                UMat thresh = FilterPlate(plate);
                threshes.Add(thresh.Clone());
            }
            return threshes;
        }

        public static UMat FilterPlate(UMat plate)
        {
            UMat thresh = new UMat();
            CvInvoke.Threshold(plate, thresh, 100, 255, ThresholdType.BinaryInv);
            CvInvoke.Erode(thresh, thresh, null, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);
            CvInvoke.Dilate(thresh, thresh, null, new Point(-1, -1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);

            return thresh;
        }


        public static Mat Resize(Mat source, Size size)
        {
            Mat dest = new Mat();
            double scale = Math.Min((float)size.Width / (float)source.Size.Width, (float)size.Height / (float)source.Size.Height);
            Size newSize = new Size((int)Math.Round(source.Size.Width * scale), (int)Math.Round(source.Size.Height * scale));
            CvInvoke.Resize(source, dest, newSize, 0, 0, Inter.Cubic);
            return dest;
        }

        public static UMat Resize(UMat source, Size size)
        {
            UMat dest = new UMat();
            double scale = Math.Min((float)size.Width / (float)source.Size.Width, (float)size.Height / (float)source.Size.Height);
            Size newSize = new Size((int)Math.Round(source.Size.Width * scale), (int)Math.Round(source.Size.Height * scale));
            CvInvoke.Resize(source, dest, newSize, 0, 0, Inter.Cubic);
            return dest;
        }

        public static List<UMat> Resize(List<UMat> sources, Size size)
        {
            List<UMat> dest = new List<UMat>();
            foreach (var src in sources)
            {
                dest.Add(Resize(src, size));
            }
            return dest;
        }

        public static List<UMat> GetImagesInContours(List<RotatedRect> boxes, Mat from)
        {
            List<UMat> imagesMat = new List<UMat>();
            foreach (var box in boxes)
            {
                float height = Math.Max(box.Size.Height, box.Size.Width);
                float width = Math.Min(box.Size.Height, box.Size.Width);
                //float height = box.Size.Height;
                //float width = box.Size.Width;
                PointF[] destCorners;
                if (box.Size.Height > box.Size.Width)
                {
                    destCorners = new PointF[] {
                        new PointF(0, height - 1),
                        new PointF(0, 0),
                        new PointF(width - 1, 0),
                        new PointF(width - 1, height - 1)};
                }
                else
                {
                    destCorners = new PointF[] {
                        new PointF(width - 1, height - 1),
                        new PointF(0, height - 1),
                        new PointF(0, 0),
                        new PointF(width - 1, 0)};
                }
                PointF[] srcCorners = box.GetVertices();


                using (Mat rot = CvInvoke.GetAffineTransform(srcCorners, destCorners))
                using (UMat tmp1 = new UMat())
                {
                    CvInvoke.WarpAffine(from, tmp1, rot, Size.Round(new SizeF(width, height)));
                    imagesMat.Add(tmp1.Clone());
                }
            }
            return imagesMat;
        }

        public static List<Bitmap> UmatsToBitmaps(List<UMat> umats)
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            foreach (var umat in umats)
            {
                bitmaps.Add((Bitmap)umat.Bitmap.Clone());
            }
            return bitmaps;
        }
    }
}
