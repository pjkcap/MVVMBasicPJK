using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MVVMBasicPJK.Common.Interfaces
{
    public interface INavigationService
    {
        Task NavigateAsync(Page page);
        Task NavigateBack();
    }
}
