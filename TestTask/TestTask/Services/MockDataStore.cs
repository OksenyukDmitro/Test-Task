using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
               new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Translate="Translate", Tag="#" +"One"  , Transcript = "Transcript"+ " item" },
            };

            for (int i = 0; i < 25; i++)
            {
                mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = i + " item", Translate = "Translate", Tag = "#" + i + "ss", Transcript = "Transcript" + i + " item" });
            }

            for (int i = 0; i < 35; i++)
            {
                mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = i + " item", Translate = "Translate", Tag = "#" + i + "ss" , Transcript = "Transcript"+ i + " item" });
            }

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Insert(0, item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Insert(0, item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}