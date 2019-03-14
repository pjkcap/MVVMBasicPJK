using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMBasicPJK.Views
{

    public class MainPageMenuItem
    {
        public MainPageMenuItem()
        {
            //TargetType = typeof(MainPageDetail);
        }
        public MenuItemType Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }

    public enum MenuItemType
    {
        Page1,
        About
    }

}