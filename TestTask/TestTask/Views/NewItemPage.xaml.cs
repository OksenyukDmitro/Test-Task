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
                Text = "",
                Translate = "",
                Tag = tags,
                Transcript = "",
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Item.Text) || string.IsNullOrEmpty(Item.Translate) || string.IsNullOrEmpty(Item.Transcript) || Item.Tag.Count == 0)
            {
                if (Text.PlaceholderColor == Color.Red)
                {
                    Text.PlaceholderColor = Color.Yellow;
                    Transcript.PlaceholderColor = Color.Yellow;
                    Translate.PlaceholderColor = Color.Yellow;
                    NewTag.PlaceholderColor = Color.Yellow;
                }
                else
                {
                    Text.PlaceholderColor = Color.Red;
                    Transcript.PlaceholderColor = Color.Red;
                    Translate.PlaceholderColor = Color.Red;
                    NewTag.PlaceholderColor = Color.Red;
                }
                return;
            }               
             MessagingCenter.Send(this, "AddItem", Item);
             await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        void AddTag_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewTag.Text))
            {
                if(NewTag.PlaceholderColor == Color.Red)
                {
                    NewTag.PlaceholderColor = Color.Yellow;
                }
                else
                {
                    NewTag.PlaceholderColor = Color.Red;
                }
                return;
            }
            tags.Add("#" + NewTag.Text);          
            AllTag.Text += NewTag.Text + " ";
            NewTag.Text = "";
        }
    }
}