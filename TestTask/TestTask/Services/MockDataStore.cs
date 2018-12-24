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

            
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "meat", Translate = "м'ясо", Tag = new List<string>() { "#" + "food", "#" + "emotions", "#" + "emotions" }, Transcript = "mēt" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "beef", Translate = "яловичина", Tag = new List<string>() { "#" + "food", "#" + "people" }, Transcript = "bēf" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "angry", Translate = "сердитий", Tag = new List<string>() { "#" + "emotions", "#" + "people" }, Transcript = "ˈæŋɡri" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "dreamy", Translate = "мрійливий", Tag = new List<string>() { "#" + "emotions", "#" + "weather" }, Transcript = "ˈdriːmi" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "unhappy", Translate = "нещасний", Tag = new List<string>() { "#" + "emotions", "#" + "family" }, Transcript = "ʌnˈhapi" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "cloud", Translate = "хмара", Tag = new List<string>() { "#" + "weather", "#" + "merry" }, Transcript = "klaʊd" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "foggy", Translate = "туманний", Tag = new List<string>() { "#" + "weather" }, Transcript = "ˈfôgē" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "hot", Translate = "жаркий", Tag = new List<string>() { "#" + "weather" }, Transcript = "hät" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "thunder", Translate = "грім", Tag = new List<string>() { "#" + "weather" }, Transcript = "ˈθʌndər" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "beetroot", Translate = "буряк", Tag = new List<string>() { "#" + "food" }, Transcript = "ˈbiːtruːt" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "onion", Translate = "цибуля", Tag = new List<string>() { "#" + "food" }, Transcript = "ˈənyən" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "radish", Translate = "редька", Tag = new List<string>() { "#" + "food" }, Transcript = "ˈrædɪʃ" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "brother", Translate = "брат", Tag = new List<string>() { "#" + "family" }, Transcript = "ˈbrʌðə" });
            mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = "cousin", Translate = "двоюрідний брат", Tag = new List<string>() { "#" + "family" }, Transcript = "kəzən" });
            
            for (int i = 0; i < 15; i++)
            {
                mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = i + " item", Translate = "Translate", Tag = new List<string>() { "#" + i + "ss" }, Transcript = "Transcript"+ i + " item" });
            }
            for (int i = 0; i < 15; i++)
            {
                mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = i + " item", Translate = "Translate", Tag = new List<string>() { "#" + i + "ss" }, Transcript = "Transcript" + i + " item" });
            }
            for (int i = 0; i < 15; i++)
            {
                mockItems.Add(new Item { Id = Guid.NewGuid().ToString(), Text = i + " item", Translate = "Translate", Tag = new List<string>() { "#" + i + "ss" }, Transcript = "Transcript" + i + " item" });
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