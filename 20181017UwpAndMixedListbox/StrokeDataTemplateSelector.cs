using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace _20181017UwpAndMixedListbox
{
    public class StrokeDataTemplateSelector : DataTemplateSelector
    {
        public ObservableCollection<TemplateMatch> Matches { get; set; }
            = new ObservableCollection<TemplateMatch>();

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return Matches.FirstOrDefault(m => m.TargetType.Equals(item.GetType().ToString()))
                .Template;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return this.SelectTemplateCore(item);
        }
    }
}
