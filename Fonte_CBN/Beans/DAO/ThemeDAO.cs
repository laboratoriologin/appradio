using RadioPlayer.Model.Beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace RadioPlayer.Model.DAO
{
    public class ThemeDAO
    {
        private Uri baseUri;

        public ThemeDAO(Uri baseUri)
        {
            this.baseUri = baseUri;
        }

        public List<Theme> getThemeList()
        {
            List<Theme> list = new List<Theme>();
            
            Theme blueTheme = new Theme();
            blueTheme.Title = "Azul";
            blueTheme.BackgroundImg = new Uri(baseUri, @"Assets/bg_azul.png");
            blueTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/background_snap_blue.png");
            blueTheme.LogoImg = new Uri(baseUri, @"Assets/TitleLogo_azul.png");
            blueTheme.ItemColor = new Color() { A = 0xFF, R = 0x48, G = 0xC2, B = 0xC5 };
            blueTheme.FontColor = new Color() { A = 0xFF, R = 0xFF, G = 0xFF, B = 0xFF };
            blueTheme.Id = Preference.BLUE_THEME;
            list.Add(blueTheme);

            Theme orangeTheme = new Theme();
            orangeTheme.Title = "Laranja";
            orangeTheme.BackgroundImg = new Uri(baseUri, @"Assets/bg_laranja.png");
            orangeTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/background_snap_orange.png");
            orangeTheme.LogoImg = new Uri(baseUri, @"Assets/TitleLogo_laranja.png");
            orangeTheme.ItemColor = new Color() { A = 0xFF, R = 0xFE, G = 0xB5, B = 0x44 };
            orangeTheme.FontColor = new Color() { A = 0xFF, R = 0xFF, G = 0xFF, B = 0xFF };
            orangeTheme.Id = Preference.ORANGE_THEME;
            list.Add(orangeTheme);

            Theme curvesTheme = new Theme();
            curvesTheme.Title = "Curvas";
            curvesTheme.BackgroundImg = new Uri(baseUri, @"Assets/bg_curvas.png");
            curvesTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/background_snap_curves.png");
            curvesTheme.LogoImg = new Uri(baseUri, @"Assets/TitleLogo_curvas.png");
            curvesTheme.ItemColor = new Color() { A = 0x4D, R = 0x00, G = 0x00, B = 0x00 };
            curvesTheme.FontColor = new Color() { A = 0xFF, R = 0x54, G = 0x3F, B = 0x2D };            
            curvesTheme.Id = Preference.CURVES_THEME;
            list.Add(curvesTheme);

            Theme bubblesTheme = new Theme();
            bubblesTheme.Title = "Bolhas";
            bubblesTheme.BackgroundImg = new Uri(baseUri, @"Assets/bg_bolhas.png");
            bubblesTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/background_snap_bubble.png");
            bubblesTheme.LogoImg = new Uri(baseUri, @"Assets/TitleLogo_bolhas.png");
            bubblesTheme.ItemColor = new Color() { A = 0x4D, R = 0x00, G = 0x00, B = 0x00 };
            bubblesTheme.FontColor = new Color() { A = 0xFF, R = 0xFF, G = 0xFF, B = 0xFF };
            bubblesTheme.Id = Preference.BUBBLE_THEME;
            list.Add(bubblesTheme);

            Theme floresTheme = new Theme();
            floresTheme.Title = "Flores";
            floresTheme.BackgroundImg = new Uri(baseUri, @"Assets/bg_flores.png");
            floresTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/background_snap_flower.png");
            floresTheme.LogoImg = new Uri(baseUri, @"Assets/TitleLogo_flores.png");
            floresTheme.ItemColor = new Color() { A = 0x4D, R = 0x00, G = 0x00, B = 0x00 };
            floresTheme.FontColor = new Color() { A = 0xFF, R = 0xFF, G = 0xFF, B = 0xFF };
            floresTheme.Id = Preference.FLOWER_THEME;
            list.Add(floresTheme);


            Theme bahiaFMTheme = new Theme();
            bahiaFMTheme.Title = "Bahia FM";
            bahiaFMTheme.BackgroundImg = new Uri(baseUri, @"Assets/BFM/bg_bahia.png");
            bahiaFMTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/BFM/background_snap_bahia.png");
            bahiaFMTheme.LogoImg = new Uri(baseUri, @"Assets/BFM/logo_home_bahia.png");
            bahiaFMTheme.ItemColor = new Color() { A = 0x4D, R = 0x00, G = 0x00, B = 0x00 };
            bahiaFMTheme.FontColor = new Color() { A = 255, R = 7, G = 89, B = 152 };
            bahiaFMTheme.Id = Preference.BAHIAFM_THEME;
            list.Add(bahiaFMTheme);

            Theme bahiaFMSULTheme = new Theme();
            bahiaFMSULTheme.Title = "Bahia FM Sul";
            bahiaFMSULTheme.BackgroundImg = new Uri(baseUri, @"Assets/BFM_SUL/180/Background-1920x1080px.png");
            bahiaFMSULTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/BFM_SUL/background_snap_bahiasul.png");
            bahiaFMSULTheme.LogoImg = new Uri(baseUri, @"Assets/BFM_SUL/180/Logo-na-tela-Home-302x163px.png");
            bahiaFMSULTheme.ItemColor = new Color() { A = 0x4D, R = 0x00, G = 0x00, B = 0x00 };
            bahiaFMSULTheme.FontColor = new Color() { A = 255, R = 7, G = 89, B = 152 };
            bahiaFMSULTheme.Id = Preference.BAHIAFMSul_THEME;
            list.Add(bahiaFMSULTheme);

            Theme globoFMTheme = new Theme();
            globoFMTheme.Title = "Globo FM ";
            globoFMTheme.BackgroundImg = new Uri(baseUri, @"Assets/GFM/bg_globo.png");
            globoFMTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/GFM/background_snap_globo.png");
            globoFMTheme.LogoImg = new Uri(baseUri, @"Assets/GFM/logo_home_globofmsalvador.png");
            globoFMTheme.ItemColor = new Color() { A = 0x4D, R = 0x00, G = 0x00, B = 0x00 };
            globoFMTheme.FontColor = new Color() { A = 0xFF, R = 0xFF, G = 0xFF, B = 0xFF };
            globoFMTheme.Id = Preference.GLOBOFM_THEME;
            list.Add(globoFMTheme);

            Theme cbnTheme = new Theme();
            cbnTheme.Title = "CBN";
            cbnTheme.BackgroundImg = new Uri(baseUri, @"Assets/CBN/180/Background-1920x1080px.png");
            cbnTheme.BackgroundImgSnap = new Uri(baseUri, @"Assets/background_snap_cbnfmsalvador.png");
            cbnTheme.LogoImg = new Uri(baseUri, @"Assets/CBN/80/Logo-na-tela-Home-302x163px.png");
            cbnTheme.ItemColor = new Color() { A = 0x4D, R = 0x00, G = 0x00, B = 0x00 };
            cbnTheme.FontColor = new Color() { A = 0xFF, R = 0xFF, G = 0xFF, B = 0xFF };
            cbnTheme.Id = Preference.CBN_THEME;
            list.Add(cbnTheme);

            return list;
        }

    }
}
