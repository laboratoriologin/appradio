using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RadioPlayer.Model
{
    public class Preference
    {
        private static String THEME_DATA = "theme_data";
        public static int BLUE_THEME   = 0;
        public static int ORANGE_THEME = 1;
        public static int CURVES_THEME = 2;
        public static int BUBBLE_THEME = 3;
        public static int FLOWER_THEME = 4;
        public static int BAHIAFM_THEME = 5;
        public static int BAHIAFMSul_THEME = 6;
        public static int GLOBOFM_THEME = 7;
        public static int CBN_THEME = 8;
      
        private static String FAVORITE_DATA = "favorite_data";

        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public int GetTheme()
        {
            Windows.Storage.ApplicationDataCompositeValue composite =
              (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values[THEME_DATA];

            if (composite == null)
            {
                return BLUE_THEME;
            }
            else
            {
                return (int) composite["intVal"];
            }

        }

        public void SetFavorites(List<RadioItem> favorites)
        {
            String str = "";
            foreach (RadioItem radio in favorites)
            {
                str += radio.Id + ";";
            }

            localSettings.Values[FAVORITE_DATA] = str;
        }

        public String[] GetFavorites()
        {
            String str = (String) localSettings.Values[FAVORITE_DATA];
            if (str != null)
            {
                return str.Split(';');
            }

            return null;
        }

        public void SetTheme(int theme)
        {
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
            composite["intVal"] = theme;

            localSettings.Values[THEME_DATA] = composite;
        }

    }
}
