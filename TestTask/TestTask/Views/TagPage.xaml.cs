using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TestTask.Models;
using TestTask.Views;
using TestTask.ViewModels;
using System.Collections.ObjectModel;

namespace TestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagPage : ContentPage
    {
        TagsViewModel viewModel;
        SearchBar searchBar;

        public TagPage(ref SearchBar searchBar, ObservableCollection<Item> items)
        {
            InitializeComponent();

            this.searchBar = searchBar;

            BindingContext = viewModel = new TagsViewModel(items);
        }
        
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTag = e.NewTextValue.Trim().ToLower();
            if (string.IsNullOrEmpty(searchTag))
            {
                TagsListView.ItemsSource = viewModel.Tags;
            }
            else
            {
               
                if (searchTag[0].Equals('#'))
                {
                    TagsListView.ItemsSource = viewModel.Tags.Where(x => x.Name.StartsWith(searchTag));
                }
                else
                {
                     searchTag = "#" + searchTag;
                    TagsListView.ItemsSource = viewModel.Tags.Where(x => x.Name.StartsWith(searchTag));
                }
            }
        }
         void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Tag;
            if (item == null)
                return;

            searchBar.Text = item.Name;
            Navigation.PopModalAsync();
            TagsListView.SelectedItem = null;
        }

         void Exit_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
     
       
    }
}