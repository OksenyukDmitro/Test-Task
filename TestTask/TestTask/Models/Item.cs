using System;
using System.Collections.Generic;

namespace TestTask.Models
{
    public class Item 
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Translate { get; set; }
        public List<string> Tag { get; set; }
        public string Transcript { get; set; }

       
    }
}