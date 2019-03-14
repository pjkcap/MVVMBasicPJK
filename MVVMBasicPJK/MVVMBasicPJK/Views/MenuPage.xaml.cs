using MVVMBasicPJK.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMBasicPJK.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        //public ListView ListView;
        MenuPageViewModel viewModel;

        public MenuPage()
        {
            InitializeComponent();

            //ListView = MenuItemsListView;

            BindingContext = viewModel = new MenuPageViewModel();

            MenuItemsListView.SelectedItem = viewModel.MenuItems[0];
        }
    }
}