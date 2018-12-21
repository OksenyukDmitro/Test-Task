using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace TestTask.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        string Text { get; set; }
        public AboutViewModel()
        {
            Title = "About";
            Text = "This about page";
        }

        
    }
}