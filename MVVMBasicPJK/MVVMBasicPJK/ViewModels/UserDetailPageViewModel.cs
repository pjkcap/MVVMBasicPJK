using MVVMBasicPJK.Models;
using Xamarin.Forms.Maps;
using System.Linq;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Messaging;
using System.ComponentModel;
using MVVMBasicPJK.Common.Helpers;

namespace MVVMBasicPJK.ViewModels
{
    /// <summary>
    /// UserDetailPage.xaml 페이지와 연결된 UserDetailPageViewModel class 
    /// </summary>
    /// <remarks>
    /// 사용자 상세 정보 페이지에서 사용되는 ViewModel
    /// Prism 참조 
    /// https://github.com/PrismLibrary/Prism/tree/master/docs/Xamarin-Forms
    /// Xamarin.Forms.Maps 참조
    /// https://developer.xamarin.com/guides/xamarin-forms/user-interface/map/
    /// </remarks>
    public class UserDetailPageViewModel : BaseViewModel
    {
        #region Private Fields

        private User user;
        private Command<Map> mapInitCommand;
        private Command dialNumberCommand;
        private Command messageNumberCommand;
        private Command emailCommand;
        private bool canNotSearchAddress;
        #endregion

        #region Property Area

        /// <summary>
        /// User 상세 정보
        /// </summary>
        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        /// <summary>
        /// 지도창에 주소 검색 가능 여부
        /// </summary>
        /// <value><c>true</c> 주소 검색결과가 없는 경우; 주소 검색 결과가 있는 경우 <c>false</c></value>
        public bool CanNotSearchAddress
        {
            get { return canNotSearchAddress; }
            set { SetProperty(ref canNotSearchAddress, value); }
        }
        #endregion

        #region Constructor
        public UserDetailPageViewModel()
        {

        }
        #endregion

        #region Command Area
        /// <summary>
        /// Get MapInitCommand
        /// </summary>
        /// <remarks>
        /// Map 정보 처리용
        /// </remarks>
        public Command<Map> MapInitCommand =>
                                        mapInitCommand ?? (mapInitCommand =
                                                                new Command<Map>(async (Map map) => await MapInit(map)));

        /// <summary>
        /// 전화 걸기 Command
        /// (iOS Simulator 에서는 지원이 안됨)
        /// </summary>
        public Command DialNumberCommand =>
                                        dialNumberCommand ?? (dialNumberCommand =
                                                                new Command
                                                                (
                                                                    () =>
                                                                    {
                                                                        if (string.IsNullOrWhiteSpace(User.CellPhoneNumber))
                                                                            return;

                                                                        var phoneCallTask = CrossMessaging.Current.PhoneDialer;
                                                                        if (phoneCallTask.CanMakePhoneCall)
                                                                            phoneCallTask.MakePhoneCall(User.CellPhoneNumber.OnlyDigitNumber());
                                                                    }
                                                                ));
        /// <summary>
        /// SMS 발송 하기 Command
        /// (iOS Simulator 에서는 지원이 안됨)
        /// </summary>
        public Command MessageNumberCommand =>
                                       messageNumberCommand ?? (messageNumberCommand =
                                                               new Command
                                                               (
                                                                   () =>
                                                                   {
                                                                       if (string.IsNullOrWhiteSpace(User.CellPhoneNumber))
                                                                           return;

                                                                       var messageTask = CrossMessaging.Current.SmsMessenger;
                                                                       if (messageTask.CanSendSms)
                                                                           messageTask.SendSms(User.CellPhoneNumber.OnlyDigitNumber());
                                                                   }
                                                               ));
        /// <summary>
        /// Email 보내기 Command
        /// (iOS Simulator 에서는 지원이 안됨)
        /// </summary>
        public Command EmailCommand =>
                                        emailCommand ?? (emailCommand =
                                                                new Command
                                                                (
                                                                    () =>
                                                                    {
                                                                        if (string.IsNullOrWhiteSpace(User.Email))
                                                                            return;

                                                                        var emailTask = CrossMessaging.Current.EmailMessenger;
                                                                        if (emailTask.CanSendEmail)
                                                                            emailTask.SendEmail(User.Email);
                                                                    }
                                                                ));
        #endregion

        #region 주소 검색용 Private Method
        /// <summary>
        /// 사용자 주소정보를 이용하여 지도에 표시하기
        /// </summary>
        /// <param name="map">Map instance</param>
        private async Task MapInit(Map map)
        {
            if (map is null)
                return;
            IsBusy = true;
            try
            {
                var geocoder = new Geocoder();
                var address = $"{User.Address.Street}, {User.Address.City}, {User.Address.State}";

                /*
                 * 주소 검색으로 위치 정보 가져오기
                 * 사족 : 주소정보가 정확하지 않아서 인지는 모르겠지만 위치정보 검색률이 많이 낮네요 T.T
                 */
                var positions = await geocoder.GetPositionsForAddressAsync(address);

                if (positions?.Count() > 0)
                {
                    //주소 검색으로 찾은 위치 정보중 첫번째 위치 정보만 이용
                    var position = positions.FirstOrDefault();
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(10)));
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = $"{User.Name.First}'s Home",
                        Address = address
                    };
                    map.Pins.Add(pin);
                    CanNotSearchAddress = false;
                }
                else
                {
                    //주소 검색이 되지 않을경우 국가 코드로 검색해 보기
                    address = $"{User.Address.State} {User.Nationality}";
                    var position = (await geocoder.GetPositionsForAddressAsync(address)).FirstOrDefault();
                    if (position.Latitude == 0 && position.Longitude == 0)
                    {
                        //var position = new Position(37.7767729, -122.4188051);
                        //map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(5)));
                        //var pin = new Pin
                        //{
                        //    Type = PinType.SavedPin,
                        //    Position = position,
                        //    Label = "자마린 본사",
                        //};
                        //map.Pins.Add(pin);
                        CanNotSearchAddress = true;
                    }
                    else
                    {
                        map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(500)));
                        var pin = new Pin
                        {
                            Type = PinType.Place,
                            Position = position,
                            Label = address,
                        };
                        map.Pins.Add(pin);
                        CanNotSearchAddress = false;
                    }
                }

            }
            catch (Exception ex)
            {
                CanNotSearchAddress = true;
                Debug.WriteLine($"Error in: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
