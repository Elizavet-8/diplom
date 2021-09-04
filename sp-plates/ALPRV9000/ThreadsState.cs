using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace ALPRV9000
{
    class ThreadsState
    {
        Thread thread1;
        public ThreadsState(Thread t)
        {
            thread1 = t;
        }
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int TerminateThread(int hThread);
        public void AbordThreadWithDalay()
        {
            Thread.Sleep(1000);            
            //TerminateThread(thread1.ManagedThreadId);
            thread1.Abort();
        }
    }
}
