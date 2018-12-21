﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TestTask.Models;
using TestTask.Views;

namespace TestTask.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public string Tag { get; set; }

        public ItemsViewModel()
        {
            Title = "Word";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadItemsCommand.Execute(new Command(async () => await ExecuteLoadItemsCommand()));
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", (obj, item) =>
           {
               var newItem = item as Item;
               Items.Insert(0, newItem);
               DataStore.AddItemAsync(newItem);
           });
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