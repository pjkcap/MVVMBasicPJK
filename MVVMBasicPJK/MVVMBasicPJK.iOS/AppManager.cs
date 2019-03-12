using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Foundation;
using MVVMBasicPJK.Common.Interfaces;
using MVVMBasicPJK.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(AppManager))]
namespace MVVMBasicPJK.iOS
{
    public class AppManager : IDependencyMgr
    {
        private readonly byte[] salt = Encoding.ASCII.GetBytes("GnetProtocolWSforMobile.asmx");
        private readonly string encryptionPassword = "mobileNeOSS";

        public void CloseApp()
        {
            Process.GetCurrentProcess().CloseMainWindow();
            Process.GetCurrentProcess().Close();

            //Thread.CurrentThread.Abort();
        }
    }
}