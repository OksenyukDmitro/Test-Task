﻿using System;
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

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Translate = "This is an item description.",
                Tag = "#1",
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
    }
}