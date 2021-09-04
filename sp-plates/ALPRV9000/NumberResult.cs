using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALPRV9000
{
    class NumberResult
    {
        private string number;
        private bool alprResult;

        public NumberResult(string number, bool alprResult)
        {
            this.number = number;
            this.alprResult = alprResult;
        }

        public NumberResult()
        {
            this.number = "";
            alprResult = false;
        }

        public void Change(string number, bool alprResult)
        {
            this.number = number;
            this.alprResult = alprResult;
        }

        public string NUMBER
        {
            get { return number; }
        }

        public bool ALPR_RESULT
        {
            get { return alprResult; }
        }

    }

}
