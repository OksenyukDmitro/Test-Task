using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask.Models
{
    public enum MenuItemType
    {
        Word,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
