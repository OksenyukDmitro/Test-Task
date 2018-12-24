using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TestTask.Models;

namespace TestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        List<string> tags;
        public NewItemPage()
        {
            InitializeComponent();
            tags = new List<string>();
            Item = new Item
            {
                Text = "Item name",
                Translate = "This is an item description.",
                Tag = tags,
                Transcript = " ˈelɪmənt  neɪm ",
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
             MessagingCenter.Send(this, "AddItem", Item);
             await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        void AddTag_Clicked(object sender, EventArgs e)
        {
            tags.Add("#" + NewTag.Text);          
            AllTag.Text += NewTag.Text + " ";
            NewTag.Text = "";
        }
    }
}