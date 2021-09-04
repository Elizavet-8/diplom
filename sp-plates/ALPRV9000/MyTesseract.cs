using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Tesseract;

namespace ALPRV9000
{
    class MyTesseract
    {
        private TesseractEngine _ocr;
        public MyTesseract()
        {
            _ocr = new TesseractEngine("NumberRecognizer", "rus", EngineMode.TesseractOnly);
            _ocr.SetVariable("tessedit_char_whitelist", "АВЕКМНОРСТУХ-1234567890");
        }

        public string GetNumberFromContours(List<Bitmap> segments)
        {
            string numbers = "1234567890";
            string letters = "АВЕКМНОРСТУХ";
            string letters_numbers = letters + "-" + numbers;
            string number = "";//Тут будет номер

            for (int i = 0; i < segments.Count; i++)
            {
                string text = "";
                using (Page page = _ocr.Process(segments[i], PageSegMode.SingleChar))
                {
                    text = page.GetText();
                    number += text;
                }

            }
            number = number.Replace("\n", "");
            number = number.Replace(" ", "");
            number = number.ToUpper();
            return number;
        }


        public string GetNumberFromPlate(Bitmap plate)
        {

            string number = "";//Тут будет номер            
            _ocr.SetVariable("tessedit_char_whitelist", "АВЕКМНОРСТУХ-1234567890");
            string text = "";
            using (Page page = _ocr.Process(plate, PageSegMode.SingleLine))
            {
                text = page.GetText();
                number += text;
            }  
            return number;
        }
    }
}
