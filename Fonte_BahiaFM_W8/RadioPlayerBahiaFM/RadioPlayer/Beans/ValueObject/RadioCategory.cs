using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RadioPlayer
{
    public class RadioCategory
    {
        public string Category { get; set; }
        public string Color { get; set; }
        public List<RadioItem> Items { get; set; }
    }
}
