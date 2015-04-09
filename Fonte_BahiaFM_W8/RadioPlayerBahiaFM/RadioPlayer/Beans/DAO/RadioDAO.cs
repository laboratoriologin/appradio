using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Model.DAO
{
    public class RadioDAO
    {

        public RadioItem getRadioBahiaFM()
        {
            RadioItem itemRadio = new RadioItem();

            itemRadio = new RadioItem("Bahia", "http://sh1.upx.com.br:8096/");
            itemRadio.Id = 1;
            //itemRadio.Locality = "Salvador/BA";
            itemRadio.Locality = "";
            itemRadio.Image = new Uri("ms-appx:/Assets/BFM/140/Logo-na-tela-Home-131x158px - BFM2.png");
            itemRadio.Description = "88.7 FM";

            return itemRadio;
        }

        public List<RadioCategory> getRadiosCategory()
        {
            List<RadioCategory> categories = new List<RadioCategory>();            
            List<RadioItem> listRadio = getRadiosList();

            ////Adiciona os favoritos
            //RadioCategory categoryFavorites = new RadioCategory();
            //categoryFavorites.Category = "Minhas Estações";
            //categoryFavorites.Items = new List<RadioItem>();

            //Adiciona as rádios            
            RadioCategory categoryRadio = new RadioCategory();
            categoryRadio.Category = "Estações";
            categoryRadio.Items = new List<RadioItem>();

            Preference preference = new Preference();
            String[] indexFavorites = preference.GetFavorites();
            if (indexFavorites != null)
            {
                for (int i = 0; i < indexFavorites.Length - 1; i++)
                {
                    foreach (RadioItem item in listRadio)
                    {
                        if (item.Id == Convert.ToInt32(indexFavorites[i]))
                        {
                            item.Favorite = true;
                            //categoryFavorites.Items.Add(item);
                            break;
                        }
                    }
                }
                //foreach (RadioItem r in categoryFavorites.Items)
                //{
                //    listRadio.Remove(r);
                //}
                categoryRadio.Items = listRadio;
            }
            else
            {
                categoryRadio.Items = listRadio;
            }

            //categories.Add(categoryFavorites);
            categories.Add(categoryRadio);

            return categories;
        }

        public List<RadioItem> getRadiosList()
        {
            List<RadioItem> ListRadio = new List<RadioItem>();

            RadioItem radio;

            radio = new RadioItem("Bahia", "http://sh1.upx.com.br:8096/");
            radio.Id = 1;
            radio.Locality = "Salvador/BA";
            radio.Image = new Uri("ms-appx:/Assets/BFM/140/Logo-na-tela-Home-131x158px - BFM2.png");
            radio.Description = "88.7 FM";
            ListRadio.Add(radio);
      
            radio = new RadioItem("Bahia Sul", "http://sh2.upx.com.br:8102/");
            radio.Id = 2;
            radio.Locality = "Itabuna/BA";
            //radio.Image = new Uri("ms-appx:/Assets/bahia_sul.png");
            radio.Image = new Uri("ms-appx:/Assets/BFM SUL/140/Logo-na-tela-Home-131x158px - BFMSUL2.png");
            radio.Description = "102.1 FM";
            ListRadio.Add(radio);
      
            radio = new RadioItem("Globo", "http://sh1.upx.com.br:8098");
            radio.Id = 3;
            radio.Locality = "Salvador/BA";
            radio.Image = new Uri("ms-appx:/Assets/GFM/100/Logo-150x50px.png");
            radio.Description = "90.1 FM";
            ListRadio.Add(radio);

            radio = new RadioItem("CBN", "http://sh1.upx.com.br:8100/");
            radio.Id = 4;
            radio.Description = "91,3 FM";
            radio.Locality = "Salvador/BA";

            //radio.Image = new Uri("ms-appx:/Assets/CBN/logo_cbnfmsalvador.jpg");
            radio.Image = new Uri("ms-appx:/Assets/CBN/180/Logo-270x270px.png");
            ListRadio.Add(radio);

            //radio = new RadioItem("A Tarde", "http://venetian.upx.net.br/d4f6");
            //radio.Description = "103.9 FM";
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/atarde.png");            
            //ListRadio.Add(radio);      

            //radio = new RadioItem("Metrópole", "mms://audiomp.radiometropole.net.br/metropolefm");
            //radio.Description = "101.3 FM";
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/logo_metropolefm.jpg");
            //ListRadio.Add(radio);


            //radio = new RadioItem("Itapoan", "http://wms.novotempohost.com:8000");
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/logo_itapoanfm.png");
            //radio.Description = "97.5 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Piatã", "http://radio.piatafm.com.br/audio");
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/logo_piatafm.jpg");
            //radio.Description = "94.3 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Transamérica", "mms://wmedialive.telium.com.br/transbapop");
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/transamerica.jpg");
            //radio.Description = "100.1 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Educadora", "mms://200.187.60.99/irdeb");
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/logo_educadorabafm.jpg");
            //radio.Description = "107.5 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Maré", "mms://omegasistemas.com.br/marefm");
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/logo_marefm.jpg");
            //radio.Description = "87.9 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Salmos", "http://184.154.89.186:9942");
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/logo_salmosfm.png");
            //radio.Description = "102.1 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Sucesso", "mms://redesucesso2.dnip.com.br/sucesso931");
            //radio.Locality = "Salvador/BA";
            //radio.Image = new Uri("ms-appx:/Assets/logo_sucessofm.jpg");
            //radio.Description = "93.1 FM";
            //ListRadio.Add(radio);

            ////Radios Sergipe
            //radio = new RadioItem("UFS", "mms://midia2.infonet.com.br/ufsfm");
            //radio.Locality = "Aracaju/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_ufssefm.jpg");
            //radio.Description = "92.1 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Jovempan", "http://66.185.27.98:8018/");
            //radio.Locality = "Aracaju/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_jpsefm.png");
            //radio.Description = "88.7 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Jubileu", "http://173.192.228.34:9064/");
            //radio.Locality = "Aracaju/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_jubileusefm.png");
            //radio.Description = "105.9 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Aparecida", "mms://midia.infonet.com.br/aparecidafm");
            //radio.Locality = "Lagarto/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_aparecidasefm.jpg");
            //radio.Description = "94.7 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Eldorado", "mms://eldorado.dnip.com.br/eldoradoaudio");
            //radio.Locality = "Lagarto/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_eldoradosefm.jpg");
            //radio.Description = "100.7 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Itabaiana", "http://184.154.106.110:8004/stream?.wma");
            //radio.Locality = "Itabaiana/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_itabaianasefm.jpg");
            //radio.Description = "93.1 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Princesa", "mms://midia.infonet.com.br/princesafm");
            //radio.Locality = "Itabaiana/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_princesasefm.png");
            //radio.Description = "99.3 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Tropical", "http://208.115.218.74:8024/");
            //radio.Locality = "Simão Dias/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_tropicalsefm.png");
            //radio.Description = "104.9 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Luandê", "http://173.192.228.34:9026");
            //radio.Locality = "Tobias Barreto/SE";
            //radio.Image = new Uri("ms-appx:/Assets/logo_luandesefm.gif");
            //radio.Description = "96.1 FM";
            //ListRadio.Add(radio);

            ////São Paulo
            //radio = new RadioItem("Antena 1", "http://s9.mediastreaming.it:7050/");
            //radio.Locality = "São Paulo/SP";
            //radio.Image = new Uri("ms-appx:/Assets/logo_antena1spfm.gif");
            //radio.Description = "94.7 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Transamérica", "http://shoutcast.telium.com.br:8000/");
            //radio.Locality = "São Paulo/SP";
            //radio.Image = new Uri("ms-appx:/Assets/transamerica.jpg");
            //radio.Description = "100.1 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Alpha", "http://174-37-58-160.webnow.net.br/alphafm56k.mp3");
            //radio.Locality = "São Paulo/SP";
            //radio.Image = new Uri("ms-appx:/Assets/logo_alphaspfm.jpg");
            //radio.Description = "101.7 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Metropolitana", "http://p.mm.uol.com.br/metropolitana_alta");
            //radio.Locality = "São Paulo/SP";
            //radio.Image = new Uri("ms-appx:/Assets/logo_metropolitanaspfm.jpg");
            //radio.Description = "98.5 FM";
            //ListRadio.Add(radio);

            //radio = new RadioItem("Eldorado", "rtsp://wms1.estadao.com.br/RadioFMBL");
            //radio.Locality = "São Paulo/SP";
            //radio.Image = new Uri("ms-appx:/Assets/logo_eldoradospfm.jpg");
            //radio.Description = "107.3 FM";
            //ListRadio.Add(radio);

            ////Rio Grande do Sul
            //radio = new RadioItem("Rádio Atlântida", "http://streaming.clicrbs.com.br/atlantidaRS?channel=254");
            //radio.Locality = "Porto Alegre/RS";
            //radio.Image = new Uri("ms-appx:/Assets/logo_atlantidarsfm.png");
            //radio.Description = "94.3 FM";
            //ListRadio.Add(radio);

            ////Curitiba 
            //radio = new RadioItem("Estação Pop", "mms://200.175.8.14/estacaopop64");
            //radio.Locality = "Curitiba/PR";
            //radio.Image = new Uri("ms-appx:/Assets/logo_estacaopopwebradio.jpg");
            //radio.Description = "Webrádio";
            //ListRadio.Add(radio);

            ////Santa Catarina
            //radio = new RadioItem("Webrádio Interativa", "http://184.154.37.131:8572/");
            //radio.Image = new Uri("ms-appx:/Assets/logo_webradiointerativasp.png");
            //radio.Locality = "Joinville/SC";
            //radio.Description = "Webrádio";
            //ListRadio.Add(radio);

            for (int i = 0; i < ListRadio.Count; i++)
            {
                RadioItem item = ListRadio.ElementAt(i);
                item.Id = i + 1;
            }

            return ListRadio;
        }

    }
}
