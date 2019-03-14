using MVVMBasicPJK.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVMBasicPJK.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public ListView ListView;

        public MenuPage()
        {
            InitializeComponent();

            ListView = MenuItemsListView;
            BindingContext = new MenuPageViewModel(this);
        }
    }
}