using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using MVVMBasicPJK.Common.Interfaces;
using MVVMBasicPJK.Models;
using System.ComponentModel;
using MVVMBasicPJK.Common.Navigations;
using MVVMBasicPJK.DataServices;
using MVVMBasicPJK.DataServices.Base;
using System.Runtime.CompilerServices;
using MVVMBasicPJK.Common.Helpers;
using MVVMBasicPJK.Views;

namespace MVVMBasicPJK.ViewModels
{
    /// <summary>
    /// MainPage.xaml 페이지와 연결된 MainlPageViewModel class 
    /// </summary>
    /// <remarks>
    /// 회원 리스트 페이지에서 사용되는 ViewModel
    /// Prism 참조 
    /// https://github.com/PrismLibrary/Prism/tree/master/docs/Xamarin-Forms
    /// Xamarin.Forms.Maps 참조
    /// https://developer.xamarin.com/guides/xamarin-forms/user-interface/map/
    /// </remarks>
    public class UserListPageViewModel : BaseViewModel
    {
        #region private fields
        private INavigationService navigationService;
        private IUserService userService;
        private int page = 1;
        private const int resultCnt = 20;   //회원 리스트 페이지당 결과갯수
        private bool isRefreshing;
        private Command refreshCommand;
        private Command<User> dataLoadCommand;
        private Command<User> userSelectedCommand;
        #endregion

        #region Property area

        /// <summary>
        /// Get or set the "Users" property
        /// </summary>
        /// <value>User 리스트</value>
        public ObservableRangeCollection<User> Users { get; set; } = new ObservableRangeCollection<User>();

        /// <summary>
        /// Get or set the "IsRefreshing" property
        /// </summary>
        /// <value><c>trun</c> if this data is reloaded; otherwise, <c>false</c>.</value>
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged();
                    IsNotBusy = !isRefreshing;
                }
            }
        }
        #endregion

        #region Constructor Area
        public UserListPageViewModel()
        {
            this.navigationService = new NavigationService();
            this.userService = new UserService(new RequestProvider());

            Task.Run(async () =>
            {
                IsRefreshing = true;

                await LoadData();

                IsRefreshing = false;
            }
                    ).Wait();
        }

        //public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IUserService userService)
        //{
        //    this.navigationService = navigationService;
        //    this.dialogService = dialogService;
        //    this.userService = userService;
        //    Task.Run(async () =>
        //    {
        //        IsRefreshing = true;

        //        await LoadData();

        //        IsRefreshing = false;
        //    }
        //            ).Wait();
        //}
        #endregion

        #region Command Area

        /// <summary>
        /// Get RefleshCommand
        /// </summary>
        /// <remarks>
        /// 참조 : Prism (https://github.com/PrismLibrary/Prism) DelegateCommand
        /// Pull to refresh 이벤트 처리용 
        /// </remarks>
        public Command RefreshCommand =>
                                refreshCommand ?? (refreshCommand =
                                                    new Command
                                                    (
                                                        async () =>
                                                        {
                                                            if (isRefreshing || IsBusy)
                                                                return;

                                                            IsRefreshing = true;

                                                            page = 1;
                                                            await LoadData();

                                                            IsRefreshing = false;
                                                        },
                                                        () => IsNotBusy
                                                    )
                                                );
        /// <summary>
        /// Get DataLoadCommand
        /// </summary>
        /// <remarks>
        /// ListView ItemAppearing 이벤트 처리용
        /// </remarks>
        public Command<User> DataLoadCommand =>
                                 dataLoadCommand ?? (dataLoadCommand =
                                                    new Command<User>
                                                    (
                                                        async (User user) =>
                                                        {
                                                            if (IsBusy || Users.Count == 0)
                                                                return;

                                                            IsBusy = true;

                                                            if (user == Users[Users.Count - 1])
                                                            {
                                                                await LoadData();
                                                            }

                                                            IsBusy = false;
                                                        },
                                                        (user) => IsNotBusy
                                                    )
                                                );


        /// <summary>
        /// Get UserSelectedCommand
        /// </summary>
        /// <remarks>
        /// ListView ItemTapped 이벤트 처리용
        /// </remarks>
        public Command<User> UserSelectedCommand =>
                                        userSelectedCommand ?? (userSelectedCommand =
                                                                new Command<User>
                                                                (
                                                                    async (User user) =>
                                                                    {
                                                                        var detailPage = new UserDetailPage();
                                                                        var detailPageViewModel = new UserDetailPageViewModel();
                                                                        detailPageViewModel.User = user;
                                                                        detailPage.BindingContext = detailPageViewModel;

                                                                        await navigationService.NavigateAsync(detailPage);
                                                                    },
                                                                    (User) => IsNotBusy
                                                                )
                                                            );
        #endregion

        #region Private Method
        private async Task LoadData()
        {
            try
            {
                var users = await userService.GetUsersAsync(page, resultCnt, "xamarinkorea", false);
                if (users?.Count > 0)
                {
                    if (page == 1)
                        Users.Clear();

                    Users.AddRange(users);
                    page++;
                }
            }
            catch (Exception ex) when (ex is WebException || ex is HttpRequestException)
            {
                //await dialogService.DisplayAlertAsync("네트웍 에러 입니다.", "에러", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in: {ex}");
            }
        }
        #endregion
    }
}
