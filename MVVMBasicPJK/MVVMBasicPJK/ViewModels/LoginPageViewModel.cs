using MVVMBasicPJK.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVVMBasicPJK.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        //버전정보
        string appVer = string.Empty;
        public string AppVer
        {
            get { return appVer; }
            set { SetProperty(ref appVer, value); }
        }

        //Infomation 메시지
        bool infoShow = false;
        public bool InfoShow
        {
            get { return infoShow; }
            set { SetProperty(ref infoShow, value); }
        }

        string infoMessage = string.Empty;
        public string InfoMessage
        {
            get { return infoMessage; }
            set { SetProperty(ref infoMessage, value); }
        }

        bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        string indicatorText = string.Empty;
        public string IndicatorText
        {
            get { return indicatorText; }
            set { SetProperty(ref indicatorText, value); }
        }

        public LoginPageViewModel()
        {
            InfoMessage = string.Empty;
        }

        #region 로그인
        public bool Login()
        {
            bool bRtn = false;

            try
            {
                if (true)
                {
                    bRtn = true;
                }
                else
                {
                    this.InfoMessage = "로그인 실패하였습니다.";
                    bRtn = false;
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                //App.ShowMessageBox("에러", Constants.ErrorMessage, "확인");
                //bRtn = false;
            }
            finally
            {

            }

            return bRtn;

        }

        // 로그인 정보를 글로벌 변수에 설정한다.
        private void SetLoginInfo()
        {
        }
        #endregion


    }
}
