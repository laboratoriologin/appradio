using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer
{
    public class RadioItem 
    {
        public int Id { get; set; }       
        public string Title { get; set; }
        public string Locality { get; set; }
        public string Description { get; set; }
        public Uri Image { get; set; }
        public string Url { get; set; }
        public string Color { get; set; }
        public bool Favorite { get; set; }   

        public RadioItem() { }

        public RadioItem(string name)
        {
            Title = name;
        }

        public RadioItem(string name, string url)
        {
            Title = name;
            Url = url;
        }

    }
}
