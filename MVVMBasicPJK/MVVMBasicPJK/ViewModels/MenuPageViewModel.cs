using MVVMBasicPJK.Common.Helpers;
using MVVMBasicPJK.Models;
using MVVMBasicPJK.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MVVMBasicPJK.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        #region private fields
        private Command<MainPageMenuItem> menuSelectedCommand;
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        //private MainPageMenuItem menuSelectedItem;
        #endregion

        #region Property area
        public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

        //public MainPageMenuItem MenuSelectedItem
        //{
        //    get { return menuSelectedItem; }
        //    set { SetProperty(ref menuSelectedItem, value); }
        //}

        #endregion

        #region Constructor Area
        public MenuPageViewModel()
        {
            MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
            {
                    new MainPageMenuItem { Id = MenuItemType.UserList, Title = "UseList", TargetType = typeof(UserListPage) },
                    new MainPageMenuItem { Id = MenuItemType.Page1, Title = "Page 1", TargetType = typeof(MainPageDetail) },
                    new MainPageMenuItem { Id = MenuItemType.About, Title = "About", TargetType = typeof(AboutPage)},
                });

            //MenuSelectedItem = MenuItems[0];
        }
        #endregion

        #region Command Area

        /// <summary>
        /// Get UserSelectedCommand
        /// </summary>
        /// <remarks>
        /// ListView ItemTapped 이벤트 처리용
        /// </remarks>
        public Command<MainPageMenuItem> MenuSelectedCommand =>
                                        menuSelectedCommand ?? (menuSelectedCommand =
                                                                new Command<MainPageMenuItem>
                                                                (
                                                                    async (MainPageMenuItem menuItem) =>
                                                                    {
                                                                        await RootPage.NavigateFromMenu(menuItem);
                                                                    }
                                                                )
                                                            );
        #endregion
    }
}
