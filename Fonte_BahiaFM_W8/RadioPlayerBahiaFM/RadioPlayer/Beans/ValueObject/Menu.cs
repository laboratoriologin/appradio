using RadioPlayer.Beans.ValueObject.ValueObject.View.Principal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace RadioPlayer.Model.Beans
{
    public class Menu
    {
        public string Categoria { get; set; }
        public string FontColor { get; set; }
        public string ImageGrid { get; set; }
        public ObservableCollection<ItemVO> Lista { get; set; }


    }
}
