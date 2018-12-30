using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TestTask.Models;
using TestTask.ViewModels;
using System.Text.RegularExpressions;

namespace TestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;
        }

        async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            Item item = this.viewModel.Item;
            var answer = await DisplayAlert("Delete", "Do You want delete current word", "Yes", "No");
            if (answer)
            {
               MessagingCenter.Send(this, "DeleteItem", item);
               await Navigation.PopAsync();
            }
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            EntryText.Text = EntryText.Text.Trim().ToLower();
            EntryTranscript.Text = EntryTranscript.Text.Trim().ToLower();
            EntryTranslate.Text = EntryTranslate.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(viewModel.Item.Text) || string.IsNullOrEmpty(viewModel.Item.Translate) || string.IsNullOrEmpty(viewModel.Item.Transcript) || viewModel.Item.Tag.Count == 0)
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
            var answer = await DisplayAlert("Delete", "Do You want update current word", "Yes", "No");
            if (answer)
            {
                MessagingCenter.Send(this, "UpdateItem", viewModel.Item);
                await Navigation.PopAsync();
            }
           
        }
        void AddTag_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EntryNewTag.Text))
            {
                if (EntryNewTag.PlaceholderColor == Color.Red)
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
            viewModel.Item.Tag.Add("#" + EntryNewTag.Text);
           
            ItemListView.ItemsSource = null;
            ItemListView.ItemsSource = viewModel.Item.Tag;
            EntryNewTag.Text = "";
        }
        void EntryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EntryText.Text.Length > 0 && EntryText.Text[EntryText.Text.Length - 1] == (' '))
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