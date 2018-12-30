using System;
using System.Windows.Input;
using TestTask.Models;
using Xamarin.Forms;

namespace TestTask.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
      
        public object MessagingCentre { get; private set; }

        public ItemDetailViewModel(Item item = null)
        {
            
            Title = item?.Text;
            Item = item;
        }
        
    }
}
