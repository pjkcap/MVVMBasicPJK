using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Platform;

namespace MVVMBasicPJK.Droid
{
    [Activity(Label = "MVVMBasicPJK", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //FFImageLoading 초기화
            CachedImageRenderer.Init(true);
            //Xamarin Forms Map 초기화 하기
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            LoadApplication(new App());
        }
    }
}