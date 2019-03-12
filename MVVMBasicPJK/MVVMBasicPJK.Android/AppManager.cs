using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MVVMBasicPJK.Common.Interfaces;
using MVVMBasicPJK.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AppManager))]
namespace MVVMBasicPJK.Droid
{
    public class AppManager : IDependencyMgr
    {
        private readonly byte[] salt = Encoding.ASCII.GetBytes("GnetProtocolWSforMobile.asmx");
        private readonly string encryptionPassword = "mobileNeOSS";

        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}