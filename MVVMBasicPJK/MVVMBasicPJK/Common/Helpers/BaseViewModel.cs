using Xamarin.Forms;

namespace MVVMBasicPJK.Common.Helpers
{
    public class BaseViewModel : ObservableObject
    {
        bool isBusy = false;
        bool isNotBusy = false;

        #region Property Area
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:MVVMBasicPJK.Common.Helpers.BaseViewModel"/> is busy.
        /// </summary>
        /// <value><c>true</c> if is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (SetProperty(ref isBusy, value))
                    IsNotBusy = !isBusy;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:MVVMBasicPJK.Common.Helpers.BaseViewModel"/> is not busy.
        /// </summary>
        /// <value><c>true</c> if is not busy; otherwise, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get
            {
                return isNotBusy;
            }
            set
            {
                if (SetProperty(ref isNotBusy, value))
                    IsBusy = !isNotBusy;
            }
        }
        #endregion

        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;

        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

    }
}

