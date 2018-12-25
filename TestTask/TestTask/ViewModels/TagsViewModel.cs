using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.Views;
using Xamarin.Forms;

namespace TestTask.ViewModels
{
    class TagsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Item Item { get; set; }

        public TagsViewModel(ObservableCollection<Item> items)
        {
            Title = "Tags";
            Items = items;
            Tags = new ObservableCollection<Tag>();
            ExecuteTagInitCommand();
        }

        public TagsViewModel(Item items = null)
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand.Execute(new Command(async () => await ExecuteLoadItemsCommand()));
            Item = items;
            ExecuteTagInitCommand();
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

        private void ExecuteTagInitCommand()
        {
            Tags = new ObservableCollection<Tag>();
            

            for (int i = 0; i < Items.Count; i++)
            {
                bool uniqueTag = true;
                string uniqueTagString = "";


                foreach (var tag in Items[i].Tag)
                {
                    foreach (var tags in Tags)
                    {


                        if (tags.Name.Equals(tag))
                        {
                            uniqueTag = false;
                            tags.Count++;
                            break;
                        }


                    }
                    uniqueTagString = tag;

                    if (uniqueTag)
                    {
                        Tags.Add(new Tag { Name = uniqueTagString, Count = 1 });
                    }
                }

            }
        }
    }
}