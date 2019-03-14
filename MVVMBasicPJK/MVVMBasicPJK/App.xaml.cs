using MVVMBasicPJK.Common.Interfaces;
using MVVMBasicPJK.Models;
using MVVMBasicPJK.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MVVMBasicPJK
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }

        //로그인 정보 및 설정 
        public static LoginMo GlobalLoginInfo { get; set; }

        public App()
        {
            InitializeComponent();

            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage(true));
            }
            else
            {
                MainPage = new MainPage();
                //MainPage = new NavigationPage(new MainPage());
            }
        }

        public static void ShowMessageBox(string sHead, string sMsg, string sBtn)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert(sHead, sMsg, sBtn);
            });
        }

        public static void ShowMessageBoxAndExit(string sHead, string sMsg, string sBtn)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await App.Current.MainPage.DisplayAlert(sHead, sMsg, sBtn);

                //App 강제 종료.
                IDependencyMgr device = DependencyService.Get<IDependencyMgr>();
                device.CloseApp();
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
