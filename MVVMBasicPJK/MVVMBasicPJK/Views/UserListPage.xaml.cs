using MVVMBasicPJK.DataServices;
using MVVMBasicPJK.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVVMBasicPJK.Views
{
    public partial class UserListPage : ContentPage
    {
        public UserListPage()
        {
            InitializeComponent();

            this.BindingContext = new UserListPageViewModel();
        }
    }
}
