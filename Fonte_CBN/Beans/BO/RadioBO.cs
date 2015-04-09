using RadioPlayer.Model.Beans;
using RadioPlayer.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.BO
{
    public class RadioBO
    {
        private RadioDAO radioDAO;
        private Uri baseUri;
        private ThemeDAO themeDAO;

        public RadioBO(Uri baseUri){
            this.baseUri = baseUri;

            this.themeDAO = new ThemeDAO(baseUri);
            this.radioDAO = new RadioDAO();
        }

        public List<Theme> getThemesList()
        {
            return themeDAO.getThemeList();
        }

        public List<RadioItem> getRadiosList()
        {
            return radioDAO.getRadiosList();
        }

        public List<RadioCategory> getRadiosCategory()
        {
            return radioDAO.getRadiosCategory();
        }

        public RadioItem getCBNFM()
        {
            return radioDAO.getCBNFM();
        }

    }
}
