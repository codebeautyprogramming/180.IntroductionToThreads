using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.ViewModels
{
    public class ListBoxItemVM
    {
        public object Item { get; set; }
        public string DisplayText { get; set; }

        public ListBoxItemVM(object item, string displayText)
        {
            Item = item;
            DisplayText = displayText;
        }

        public ListBoxItemVM() { }
    }
}
