﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TestTask.Models;
using TestTask.Views;
using System.Windows.Input;
using System.Linq;

namespace TestTask.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand ShowTagsCommand { get; set; }
        
        public INavigation Navigation { get; set; }
        public SearchBar TextSearchBar;
        public ItemsViewModel()
        {
            Title = "Word";
            Items = new ObservableCollection<Item>();
            ShowTagsCommand = new Command(ShowTags);
            AddItemCommand = new Command(AddItem);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(new Command(async () => await ExecuteLoadItemsCommand()));
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", (obj, item) =>
           {
               var newItem = item as Item;
               Items.Insert(0, newItem);
               DataStore.AddItemAsync(newItem);
           });
            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", (obj, item) =>
            {
                var oldItem = item as Item;
                Items.Remove(oldItem);
                DataStore.DeleteItemAsync(oldItem.Id);
            });
            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "UpdateItem", async (obj, item) =>
            {
                var Item = item as Item;
                var oldItem = Items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
                var indexRemoveitem = Items.IndexOf(oldItem);
                Items.Remove(oldItem);
                if (indexRemoveitem == 0)
                {
                    Items.Insert(1, item);
                }
                else
                {
                    Items.Insert(0, item);
                }
                await DataStore.UpdateItemAsync(Item);
                 });
        }
        async private void AddItem()
        {
             await Navigation.PushAsync(new NewItemPage());
        }
        async private void ShowTags()
        {
            
               await Navigation.PushModalAsync(new TagPage(ref TextSearchBar, Items));
        }
        async Task ExecuteLoadItemsCommand()
        {
            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}