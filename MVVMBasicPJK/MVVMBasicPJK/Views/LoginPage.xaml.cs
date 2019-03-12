using MVVMBasicPJK.Common.Interfaces;
using MVVMBasicPJK.Models;
using MVVMBasicPJK.ViewModels;
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
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel viewModel;

        public LoginPage(bool bFisrt)
        {
            InitializeComponent();


            App.GlobalLoginInfo = new LoginMo();

            BindingContext = viewModel = new LoginPageViewModel();

            //입력창 초기화
            usernameEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;
            licenseNumEntry.Text = string.Empty;

        }

        #region [로그인] 버튼 클릭시
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            viewModel.InfoMessage = string.Empty;

            //ID, PASSWORD 입력 체크
            if (usernameEntry.Text.Trim().Length < 1)
            {
                await DisplayAlert("로그인", "아이디를 입력하세요.", "확인");
                return;
            }
            if (passwordEntry.Text.Trim().Length < 1)
            {
                await DisplayAlert("로그인", "비밀번호를 입력하세요.", "확인");
                return;
            }

            // OTP 인증 체크
            if (licenseNumEntry.Text.Trim().Length < 1)
            {
                //메세지 출력 : 인증번호를 입력하세요
                await DisplayAlert("로그인", "인증번호를 입력하세요.", "확인");
                return;
            }

            // OTP 형식체크(숫자6자리)
            string pattern = @"^\d{6}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(licenseNumEntry.Text, pattern))
            {
                await DisplayAlert("로그인", "인증번호는 숫자 6자리입니다.", "확인");
                return;
            }

            //SQL Injection 체크
            if (!(this.CheckInjection(this.usernameEntry.Text) || this.CheckInjection(this.passwordEntry.Text)))
            {
                await DisplayAlert("로그인", "입력값 중 특수문자가 있습니다.", "확인");
                return;
            }

            viewModel.IsBusy = true;
            viewModel.IndicatorText = "로그인중...";

            await Task.Delay(100);

            var isValid = AreCredentialsCorrect();


            if (isValid)
            {
                App.IsUserLoggedIn = true;
                App.Current.MainPage = new NavigationPage(new MainPage());
            }

            viewModel.IsBusy = false;
        }

        //로그인 비즈니스 로직...
        private bool AreCredentialsCorrect()
        {
            bool bRtn = false;

            string strID = usernameEntry.Text.Trim();
            string strPW = passwordEntry.Text.Trim();
            string strSMS = licenseNumEntry.Text.Trim();

            bRtn = viewModel.Login();

            return bRtn;
        }
        #endregion 


        #region Pysical Back Button 클릭시...
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("종료", "앱을 종료하시겠습니까?", "확인", "취소");
                if (result)
                {
                    IDependencyMgr device = DependencyService.Get<IDependencyMgr>();
                    device.CloseApp();
                }
            });

            return true;

        }
        #endregion

        #region 웹 취약점 보완 SQLInjection
        public bool CheckInjection(string parm)
        {
            try
            {
                char[] ch = parm.ToCharArray();

                for (int i = 0; i < ch.Length; i++)
                {
                    if ((ch[i] >= 33 && ch[i] <= 47) || (ch[i] >= 58 && ch[i] <= 64) || (ch[i] >= 91 && ch[i] <= 96) || (ch[i] >= 123 && ch[i] <= 126))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                viewModel.InfoMessage = "요청을 처리하지 못했습니다.\r\n관리자에게 문의해 주시기 바랍니다.";
                return false;
            }
        }
        #endregion

        private void BtnLicenseNum_Clicked(object sender, EventArgs e)
        {

        }
    }
}