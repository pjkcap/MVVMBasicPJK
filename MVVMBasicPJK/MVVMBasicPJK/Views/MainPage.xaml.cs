using MVVMBasicPJK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMBasicPJK.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
            MenuPages.Add((int)MenuItemType.UserList, (NavigationPage)Detail);
            //pageMenu.ListView.ItemSelected += ListView_ItemSelected;
        }

        public async Task NavigateFromMenu(MainPageMenuItem item)
        {
            int id = (int)item.Id;

            if (!MenuPages.ContainsKey(id))
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                MenuPages.Add(id, new NavigationPage(page));

                //switch (id)
                //{
                //    case (int)MenuItemType.Page1:
                //        MenuPages.Add(id, new NavigationPage(new MainPageDetail()));
                //        break;
                //    case (int)MenuItemType.About:
                //        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                //        break;
                //}
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        //private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MainPageMenuItem;
        //    if (item == null)
        //        return;

        //    var page = (Page)Activator.CreateInstance(item.TargetType);
        //    page.Title = item.Title;

        //    Detail = new NavigationPage(page);
        //    IsPresented = false;

        //    pageMenu.ListView.SelectedItem = null;
        //}
    }
}