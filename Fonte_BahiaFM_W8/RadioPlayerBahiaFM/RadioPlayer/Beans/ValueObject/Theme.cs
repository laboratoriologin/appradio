using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace RadioPlayer.Model.Beans
{
    public class Theme
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Uri LogoImg { get; set; }
        public Uri BackgroundImg { get; set; }
        public Uri BackgroundImgSnap { get; set; }
        public Color ItemColor { get; set; }
        public Color FontColor { get; set; }
    }
}
