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
using Xamarin.Forms.Internals;

namespace TestTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        int countItemToShow = 15;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel() { Navigation = this.Navigation, TextSearchBar = this.TextSearchBar};
            
            ItemsListView.ItemsSource = viewModel.Items.Take(countItemToShow);

            ItemsListView.ItemAppearing += (sender, e) =>
            {
                var item = (Item)e.Item;
                if (item.Text == viewModel.Items[countItemToShow-1].Text)
                {
                    countItemToShow += 15;
                    if (viewModel.Items.Count < countItemToShow)
                    {
                        countItemToShow = viewModel.Items.Count - 1;
                        ItemsListView.ItemsSource = viewModel.Items;
                    }
                    else
                    ItemsListView.ItemsSource = viewModel.Items.Take(countItemToShow);
                }
            };

           
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchTag = e.NewTextValue.Trim().ToLower();
           
            if (string.IsNullOrEmpty(searchTag))
            {
                
                countItemToShow = 15;
               ItemsListView.ItemsSource = viewModel.Items.Take(countItemToShow);
            }

            else if (searchTag[0].Equals('#'))
            {
              
                ItemsListView.ItemsSource = viewModel.Items.Where(x => x.Tag.Contains(searchTag));
            }
            else
            {
                
                ItemsListView.ItemsSource = viewModel.Items.Where(x => x.Text.StartsWith(searchTag));
            }
        }
       
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
          
          
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            ItemsListView.SelectedItem = null;
        }
        
               void OnTagSelected(object sender, SelectedItemChangedEventArgs args)
        {

            var list = sender as ListView;
            var item = args.SelectedItem as String;
            if (item == null)
                return;
            TextSearchBar.Text = item;
            list.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (viewModel.Items.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);


            }
            else if (string.IsNullOrEmpty(TextSearchBar.Text))
            {
                ItemsListView.ItemsSource = null;
                countItemToShow = 15;
                ItemsListView.ItemsSource = viewModel.Items.Take(countItemToShow);
            }
            else if (!string.IsNullOrEmpty(TextSearchBar.Text.Trim()))
            {
                if (TextSearchBar.Text.Trim()[0].Equals('#'))
                {
                    ItemsListView.ItemsSource = viewModel.Items.Where(x => x.Tag.Contains(TextSearchBar.Text.Trim().ToLower()));
                }
                else
                {
                    TextSearchBar.Text = "";
                       countItemToShow = 15;
                    ItemsListView.ItemsSource = viewModel.Items.Take(countItemToShow);
                }
            }
           
           
        }
    }
}