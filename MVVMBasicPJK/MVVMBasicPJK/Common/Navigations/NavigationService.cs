﻿using System.Linq;
using System;
using MVVMBasicPJK.Common.Interfaces;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace MVVMBasicPJK.Common.Navigations
{
    /// <summary>
    /// 간단한 네비게이션 구현
    /// </summary>
    public class NavigationService : INavigationService
    {
        public async Task NavigateAsync(Page page)
        {
            var currentPage = GetCurrentPage();
            if (currentPage?.Parent is NavigationPage)
                await currentPage.Navigation.PushAsync(page);
            else
                await currentPage.Navigation.PushModalAsync(page);
        }

        public async Task NavigateBack()
        {
            var currentPage = GetCurrentPage();
            if (currentPage?.Parent is NavigationPage)
                await currentPage.Navigation.PopAsync();
            else
                await currentPage.Navigation.PopModalAsync();
        }

        private Page GetCurrentPage()
        {
            Page currentPage;

            if (Application.Current.MainPage is MasterDetailPage)
                currentPage = ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.NavigationStack.LastOrDefault();
            else
                currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            return currentPage;
        }
    }
}
