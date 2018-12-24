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
            var mockItems = new List<Item>();

            
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "meat", Translate = "м'ясо", Tag = "#" + "food", Transcript = "mēt" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "beef", Translate = "яловичина", Tag = "#" + "food", Transcript = "bēf" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "angry", Translate = "сердитий", Tag = "#" + "emotions", Transcript = "ˈæŋɡri" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "dreamy", Translate = "мрійливий", Tag = "#" + "emotions", Transcript = "ˈdriːmi" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "unhappy", Translate = "нещасний", Tag = "#" + "emotions", Transcript = "ʌnˈhapi" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "cloud", Translate = "хмара", Tag = "#" + "weather", Transcript = "klaʊd" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "foggy", Translate = "туманний", Tag = "#" + "weather", Transcript = "ˈfôgē" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "hot", Translate = "жаркий", Tag = "#" + "weather", Transcript = "hät" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "thunder", Translate = "грім", Tag = "#" + "weather", Transcript = "ˈθʌndər" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "beetroot", Translate = "буряк", Tag = "#" + "food", Transcript = "ˈbiːtruːt" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "onion", Translate = "цибуля", Tag = "#" + "food", Transcript = "ˈənyən" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "radish", Translate = "редька", Tag = "#" + "food", Transcript = "ˈrædɪʃ" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "brother", Translate = "брат", Tag = "#" + "family", Transcript = "ˈbrʌðə" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "cousin", Translate = "двоюрідний брат", Tag = "#" + "family", Transcript = "kəzən" });

            for (int i = 0; i < 15; i++)
            {
                mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = i + " item", Translate = "Translate", Tag = "#" + i + "ss" , Transcript = "Transcript"+ i + " item" });
            }
            for (int i = 0; i < 15; i++)
            {
                mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = i + " item", Translate = "Translate", Tag = "#" + i + "ss", Transcript = "Transcript" + i + " item" });
            }
            for (int i = 0; i < 15; i++)
            {
                mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = i + " item", Translate = "Translate", Tag = "#" + i + "ss", Transcript = "Transcript" + i + " item" });
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