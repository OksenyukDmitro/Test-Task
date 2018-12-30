using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TestTask.Models;
using System.Text.RegularExpressions;

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
            EntryText.Text = EntryText.Text.Trim().ToLower();
            EntryTranscript.Text = EntryTranscript.Text.Trim().ToLower();
            EntryTranslate.Text = EntryTranslate.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(Item.Text) || string.IsNullOrEmpty(Item.Translate) || string.IsNullOrEmpty(Item.Transcript) || Item.Tag.Count == 0 )
            {
                if (EntryText.PlaceholderColor == Color.Red)
                {
                    EntryText.PlaceholderColor = Color.Yellow;
                    EntryTranscript.PlaceholderColor = Color.Yellow;
                    EntryTranslate.PlaceholderColor = Color.Yellow;
                    EntryNewTag.PlaceholderColor = Color.Yellow;
                    EntryNewTag.Text = "";
                }
                else
                {
                    EntryText.PlaceholderColor = Color.Red;
                    EntryTranscript.PlaceholderColor = Color.Red;
                    EntryTranslate.PlaceholderColor = Color.Red;
                    EntryNewTag.PlaceholderColor = Color.Red;
                    EntryNewTag.Text = "";
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
            if (string.IsNullOrEmpty(EntryNewTag.Text))
            {
                if(EntryNewTag.PlaceholderColor == Color.Red)
                {
                    EntryNewTag.PlaceholderColor = Color.Yellow;
                }
                else
                {
                    EntryNewTag.PlaceholderColor = Color.Red;
                }
                return;
            }
            EntryNewTag.Text = EntryNewTag.Text.Trim().ToLower();
           
            tags.Add("#" + EntryNewTag.Text);          
            AllTag.Text += EntryNewTag.Text + " ";
            EntryNewTag.Text = "";
        }
        void EntryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(EntryText.Text.Length > 0 && EntryText.Text[EntryText.Text.Length-1] == (' ') )
            {
                EntryText.Text = Regex.Replace(EntryText.Text, @"\s+", " ");
            }
        }

        void EntryTranscript_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EntryTranscript.Text.Length > 0 && EntryTranscript.Text[EntryTranscript.Text.Length - 1] == (' '))
                {
                    EntryTranscript.Text = Regex.Replace(EntryTranscript.Text, @"\s+", " ");
                }
        }
            void EntryTranslate_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (EntryTranslate.Text.Length > 0 && EntryTranslate.Text[EntryTranslate.Text.Length - 1] == (' '))
                    {
                         EntryTranslate.Text = Regex.Replace(EntryTranslate.Text, @"\s+", " ");
                    }
            }
            void EntryNewTag_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (EntryNewTag.Text.Length > 0 && EntryNewTag.Text[EntryNewTag.Text.Length - 1] == (' '))
                    {
                        EntryNewTag.Text = Regex.Replace(EntryNewTag.Text, @"\s+", "");
                    }
            }
        
    }
}