using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ALPRV9000
{
    class CompareResult
    {
        public CompareResult(Bitmap img1, Bitmap img2, string result, string details)
        {
            this.img1 = img1;
            this.img2 = img2;
            this.result = result;
            this.details = details;
        }
        public Bitmap img1;
        public Bitmap img2;
        public string result;
        public string details;
    }
}
