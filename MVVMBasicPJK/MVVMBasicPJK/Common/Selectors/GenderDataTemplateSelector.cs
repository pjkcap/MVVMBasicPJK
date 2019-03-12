using MVVMBasicPJK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MVVMBasicPJK.Common.Selectors
{
    /// <summary>
    /// 성별에 따른 Template Selector
    /// </summary>
    public class GenderDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Male { get; set; }
        public DataTemplate Female { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((User)item).Gender.Equals(Gender.Male) ? Male : Female;
        }
    }
}
