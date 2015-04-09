using RadioPlayer.Beans;
using RadioPlayer.Beans.BO;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.NoticiaCorreioXML;
using RadioPlayer.Beans.ValueObject.ValueObject.Xml.PodCastXML;
using RadioPlayer.Beans.ValueObject.Xml.Salvador;
using RadioPlayer.Beans.ValueObject.Xml.Promocao;
using RadioPlayer.Flyout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Xml = RadioPlayer.Beans.ValueObject.ValueObject.Xml;
using RadioPlayer.Beans.ValueObject.Xml.Programacao;

namespace RadioPlayer.Common.Architecture
{

    /// <summary>
    /// Classe principal do sistema.
    /// </summary>
    public static class Main
    {
        private static TelaMensagem telaMensagem = TelaMensagem.Instance;

        /// Atríbuto Dismissed
        /// </summary>
        private static bool dismissed = false; // Variable to track Splash screen dismissal status.
        /// <summary>
        /// Gets or sets a value indicating whether the item is value.
        /// </summary>
        public static bool Dismissed
        {
            get { return dismissed; }
            set { dismissed = value; }
        }
        #region Métodos

        /// <summary>
        /// Método que carrega os recursos do sistema.
        /// </summary>
        public static void LoadResource()
        {
            ResourceLoader resource = new ResourceLoader("Resources");
            string[] strResource = resource.GetString("0").Split('|');

            for (int i = 1; i <= Convert.ToInt32(strResource[1]); i++)
            {
                ChannelVO channelVO = new ChannelVO();

                string[] strResourceChannel = resource.GetString(i.ToString()).Split('|');
                channelVO.Id = Convert.ToInt32(strResourceChannel[0]);
                channelVO.NomeChannel = strResourceChannel[1];
                channelVO.Url = strResource[0] + channelVO.Id.ToString();

                DataSourceRadio.ListaChannelVO.Add(channelVO);

                channelVO = new ChannelVO();

                strResourceChannel = resource.GetString(i.ToString()).Split('|');
                channelVO.Id = Convert.ToInt32(strResourceChannel[0]);
                channelVO.NomeChannel = strResourceChannel[1];
                channelVO.Url = strResource[0] + channelVO.Id.ToString();

                DataSourceRadio.ListaChannelVOInactive.Add(channelVO);
            }

            resource = new ResourceLoader("ResourcesAG");
            strResource = resource.GetString("0").Split('|');

            for (int i = 1; i <= Convert.ToInt32(strResource[1]); i++)
            {
                ChannelAgendaCulturalVO channelAgendaCulturalVO = new ChannelAgendaCulturalVO();

                string[] strResourceChannel = resource.GetString(i.ToString()).Split('|');
                channelAgendaCulturalVO.Id = Convert.ToInt32(strResourceChannel[0]);
                channelAgendaCulturalVO.NomeChannel = strResourceChannel[1];
                channelAgendaCulturalVO.Url = strResource[0] + channelAgendaCulturalVO.Id.ToString();

                DataSourceRadio.ListaChannelAgendaCulturalVO.Add(channelAgendaCulturalVO);

                channelAgendaCulturalVO = new ChannelAgendaCulturalVO();

                strResourceChannel = resource.GetString(i.ToString()).Split('|');
                channelAgendaCulturalVO.Id = Convert.ToInt32(strResourceChannel[0]);
                channelAgendaCulturalVO.NomeChannel = strResourceChannel[1];
                channelAgendaCulturalVO.Url = strResource[0] + channelAgendaCulturalVO.Id.ToString();

                DataSourceRadio.ListaChannelAgendaCulturalVOInactive.Add(channelAgendaCulturalVO);
            }

            resource = new ResourceLoader("ResourcesPodCast");
            strResource = resource.GetString("0").Split('|');
            int y = 0;

            ChannelPodCastVO channelPodCastVO = new ChannelPodCastVO();

            string[] strResourceChannelPodCastVO = resource.GetString(y.ToString()).Split('|');
            channelPodCastVO.Id = 101;
            channelPodCastVO.NomeChannel = "PodCast";
            channelPodCastVO.Url = strResourceChannelPodCastVO[0];

            DataSourceRadio.ListaChannelPodCastVO.Add(channelPodCastVO);
            LoadChannelAsync(channelPodCastVO, true);

            resource = new ResourceLoader("ResourcesCorreio");
            strResource = resource.GetString("0").Split('|');


            ChannelCorreioVO channelCorreioVO = new ChannelCorreioVO();

            string[] strResourceChannelCorreio = resource.GetString(y.ToString()).Split('|');
            channelCorreioVO.Id = 100;
            channelCorreioVO.NomeChannel = "Notícias Correio";
            channelCorreioVO.Url = strResourceChannelCorreio[0];

            DataSourceRadio.ListaChannelCorreioVO.Add(channelCorreioVO);
            LoadChannelAsync(channelCorreioVO, true);


            ////
            resource = new ResourceLoader("ResourcesFalaBahia");
            strResource = resource.GetString("0").Split('|');


            ChannelSalvadorVO channelSalvadorVO = new ChannelSalvadorVO();

            string[] strResourceChannelBahiaFM = resource.GetString(y.ToString()).Split('|');
            channelSalvadorVO.Id = 105;
            channelSalvadorVO.NomeChannel = "Fala Bahia";
            channelSalvadorVO.Url = strResourceChannelBahiaFM[0];

            DataSourceRadio.ListaChannelSalvadorVO.Add(channelSalvadorVO);
            LoadChannelAsync(channelSalvadorVO, true);
            ////

            resource = new ResourceLoader("ResourcesProgramacao");
            strResource = resource.GetString("0").Split('|');


            ChannelProgramacaoVO channelProgramacaoVO = new ChannelProgramacaoVO();

            string[] strResourceChannelProgramacao = resource.GetString(y.ToString()).Split('|');
            channelProgramacaoVO.Id = 106;
            channelProgramacaoVO.NomeChannel = "Programacao";
            channelProgramacaoVO.Url = strResourceChannelProgramacao[0];

            DataSourceRadio.ListaChannelProgramacaoVO.Add(channelProgramacaoVO);
            LoadChannelAsync(channelProgramacaoVO, true);
            ////
            resource = new ResourceLoader("ResourcesPromocao");
            strResource = resource.GetString("0").Split('|');

            ChannelPromocaoVO channelPromocaoVO = new ChannelPromocaoVO();

            string[] strResourceChannelPromocaoAtivo = resource.GetString(y.ToString()).Split('|');
            channelPromocaoVO.Id = 105;
            channelPromocaoVO.NomeChannel = "Promoções";
            channelPromocaoVO.Url = strResourceChannelPromocaoAtivo[0];

            DataSourceRadio.ListaChannelPromocaoVO.Add(channelPromocaoVO);
            LoadChannelAsync(channelPromocaoVO, true);



            resource = new ResourceLoader("ResourcesPromocao");
            strResource = resource.GetString("2").Split('|');

            ChannelPromocaoVO channelPromocaoBahiaSulVO = new ChannelPromocaoVO();

            string[] strResourceChannelPromocaoBahiaSulAtivo = resource.GetString(y.ToString()).Split('|');
            channelPromocaoBahiaSulVO.Id = 105;
            channelPromocaoBahiaSulVO.NomeChannel = "Promoções";
            channelPromocaoBahiaSulVO.Url = strResourceChannelPromocaoAtivo[0];

            DataSourceRadio.ListaChannelPromocaoVO.Add(channelPromocaoBahiaSulVO);
            LoadChannelAsync(channelPromocaoBahiaSulVO, true);

            //channelCorreioVO = new ChannelCorreioVO();

            //strResourceChannel = resource.GetString(i.ToString()).Split('|');
            //channelCorreioVO.Id = Convert.ToInt32(strResourceChannel[0]);
            //channelCorreioVO.NomeChannel = strResourceChannel[1];

            //channelCorreioVO.Url = strResource[0] + channelCorreioVO.Id.ToString();




            resource = new ResourceLoader("ResourcesCinema");
            DataSourceRadio.ListaChannelCinemaVO.Add(new ChannelCinemaVO() { NomeChannel = "Filme em Cartaz", Id = "filmes_em_cartaz", Url = resource.GetString("0") });
            DataSourceRadio.ListaChannelCinemaVOInactive.Add(new ChannelCinemaVO() { NomeChannel = "Filme em Cartaz", Id = "filmes_em_cartaz", Url = resource.GetString("0") });
        }

        /// <summary>
        /// Método para carregar o channel tanto local como na net.
        /// </summary>
        /// <param name="objChannelVO">Obj channelVO</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        public static async Task LoadChannelAsync(object objChannelVO, bool refresh)
        {
            try
            {
                if (objChannelVO.GetType().Equals(typeof(ChannelVO)))
                {
                    ChannelVO channelVO = (ChannelVO)objChannelVO;
                    channelVO.IsLoaded = false;
                    await LoadChannelVOAsync(channelVO, refresh);
                    channelVO.IsLoaded = true;
                }
                else if (objChannelVO.GetType().Equals(typeof(ChannelAgendaCulturalVO)))
                {
                    ChannelAgendaCulturalVO channelVO = (ChannelAgendaCulturalVO)objChannelVO;
                    channelVO.IsLoaded = false;
                    await LoadChannelAgendaCulturalVOAsync(channelVO, refresh);
                    channelVO.IsLoaded = true;
                }
                else if (objChannelVO.GetType().Equals(typeof(ChannelCinemaVO)))
                {
                    ChannelCinemaVO channelVO = (ChannelCinemaVO)objChannelVO;
                    channelVO.IsLoaded = false;
                    await LoadChannelCinameVOAsync(channelVO, refresh);
                    channelVO.IsLoaded = true;
                }
                else if (objChannelVO.GetType().Equals(typeof(ChannelCorreioVO)))
                {
                    ChannelCorreioVO channelVO = (ChannelCorreioVO)objChannelVO;
                    channelVO.IsLoaded = false;
                    await LoadChannelCorreioVOAsync(channelVO, refresh);
                    channelVO.IsLoaded = true;
                }

                else if (objChannelVO.GetType().Equals(typeof(ChannelPodCastVO)))
                {
                    ChannelPodCastVO channelVO = (ChannelPodCastVO)objChannelVO;
                    channelVO.IsLoaded = false;
                    await LoadChannelPodCastVOAsync(channelVO, refresh);
                    channelVO.IsLoaded = true;
                }

                else if (objChannelVO.GetType().Equals(typeof(ChannelPromocaoVO)))
                {
                    ChannelPromocaoVO channelVO = (ChannelPromocaoVO)objChannelVO;
                    channelVO.IsLoaded = false;
                    await LoadChannelPromocaoVOAsync(channelVO, refresh);
                    channelVO.IsLoaded = true;
                }

                else if (objChannelVO.GetType().Equals(typeof(ChannelSalvadorVO)))
                {
                    ChannelSalvadorVO channelVO = (ChannelSalvadorVO)objChannelVO;
                    channelVO.IsLoaded = false;
                    await LoadChannelSalvadorVOAsync(channelVO, refresh);
                    channelVO.IsLoaded = true;
                }

                else if (objChannelVO.GetType().Equals(typeof(ChannelProgramacaoVO)))
                {
                    ChannelProgramacaoVO channelVO = (ChannelProgramacaoVO)objChannelVO;
                    channelVO.IsLoaded = false;
                    await LoadChannelProgramacaoVOAsync(channelVO, refresh);
                    channelVO.IsLoaded = true;
                }

            }
            catch (AggregateException aex)
            {
                List<Exception> lstExceptions = new List<Exception>();
                foreach (Exception ex in aex.InnerExceptions)
                {
                    lstExceptions.Add(ex);
                }
                telaMensagem.ShowMensagem(lstExceptions[0]);
            }
        }

        /// <summary>
        /// Método principal para carregar os dados do sistema.
        /// </summary>        
        public static void LoadMain()
        {
            //DataSourceRadio.ListaTarefaLoadDados.Clear();
            try
            {

                ChannelCinemaVO channelCinameVO = DataSourceRadio.ListaChannelCinemaVO[0];

                DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                {
                    LoadChannelAsync(channelCinameVO, true).Wait();
                }, channelCinameVO));

                foreach (ChannelVO itemChannelVO in DataSourceRadio.ListaChannelVO)
                {
                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelVO, true).Wait();
                    }, itemChannelVO));
                }

                foreach (ChannelPodCastVO itemChannelVO in DataSourceRadio.ListaChannelPodCastVO)
                {
                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelVO, true).Wait();
                    }, itemChannelVO));
                }


                foreach (ChannelAgendaCulturalVO itemChannelAgendaCulturalVO in DataSourceRadio.ListaChannelAgendaCulturalVO)
                {
                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelAgendaCulturalVO, true).Wait();

                    }, itemChannelAgendaCulturalVO));
                }

                foreach (ChannelCorreioVO itemChannelCorreioVO in DataSourceRadio.ListaChannelCorreioVO)
                {
                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelCorreioVO, true).Wait();
                    }, itemChannelCorreioVO));
                }

                foreach (ChannelSalvadorVO itemChannelSalvadorVO in DataSourceRadio.ListaChannelSalvadorVO)
                {
                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelSalvadorVO, true).Wait();
                    }, itemChannelSalvadorVO));
                }

                foreach (ChannelPromocaoVO itemChannelPromocaoVO in DataSourceRadio.ListaChannelPromocaoVO)
                {
                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelPromocaoVO, true).Wait();
                    }, itemChannelPromocaoVO));
                }
            }
            catch (AggregateException)
            {
                TelaMensagem.Instance.ShowMensagem(Constantes.ResourceErro.GetString("Conexao"));

            }

        }
     /// <summary>
        /// Método que recarrega os dados do sistema.
        /// </summary>
        /// <returns>Retorna uma tarefa do sistema</returns>
        public static void LoadMainRefresh(Type typeObject, int[] arrIdCategoria)
        {
            if (typeObject.Equals(typeof(ChannelVO)))
            {
                DataSourceRadio.ListaTarefaLoadDados.RemoveAll(item =>
                    item.AsyncState.GetType().Equals(typeof(ChannelVO)) && arrIdCategoria.Contains(((ChannelVO)item.AsyncState).Id)
                );

                List<ChannelVO> listChannelVO = DataSourceRadio.ListaChannelVO.Where<ChannelVO>(item => arrIdCategoria.Contains(item.Id)).ToList<ChannelVO>();

                foreach (ChannelVO itemChannelVO in listChannelVO)
                {
                    itemChannelVO.ListaItem.Clear();

                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelVO, true).Wait();
                    }, itemChannelVO));
                }
            }
            else if (typeObject.Equals(typeof(ChannelAgendaCulturalVO)))
            {
                DataSourceRadio.ListaTarefaLoadDados.RemoveAll(item =>
                    item.AsyncState.GetType().Equals(typeof(ChannelAgendaCulturalVO)) && arrIdCategoria.Contains(((ChannelAgendaCulturalVO)item.AsyncState).Id)
                );

                List<ChannelAgendaCulturalVO> listChannelAgendaCulturalVO = DataSourceRadio.ListaChannelAgendaCulturalVO.Where<ChannelAgendaCulturalVO>(item => arrIdCategoria.Contains(item.Id)).ToList<ChannelAgendaCulturalVO>();

                foreach (ChannelAgendaCulturalVO itemChannelAgendaCulturalVO in listChannelAgendaCulturalVO)
                {
                    itemChannelAgendaCulturalVO.ListaItem.Clear();

                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelAgendaCulturalVO, true).Wait();
                    }, itemChannelAgendaCulturalVO));
                }
            }
            else if (typeObject.Equals(typeof(ChannelCinemaVO)))
            {
                DataSourceRadio.ListaTarefaLoadDados.RemoveAll(item =>
                    item.AsyncState.GetType().Equals(typeof(ChannelCinemaVO))
                );

                DataSourceRadio.ListaChannelCinemaVO[0].Title = string.Empty;
                DataSourceRadio.ListaChannelCinemaVO[0].ListaItem.Clear();

                DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                {
                    LoadChannelAsync(DataSourceRadio.ListaChannelPodCastVO, true).Wait();
                }, DataSourceRadio.ListaChannelPodCastVO));
            }

            else if (typeObject.Equals(typeof(ChannelPodCastVO)))
            {
                DataSourceRadio.ListaTarefaLoadDados.RemoveAll(item =>
                    item.AsyncState.GetType().Equals(typeof(ChannelPodCastVO)) && arrIdCategoria.Contains(((ChannelPodCastVO)item.AsyncState).Id)
                );

                List<ChannelPodCastVO> listChannelVO = DataSourceRadio.ListaChannelPodCastVO.Where<ChannelPodCastVO>(item => arrIdCategoria.Contains(item.Id)).ToList<ChannelPodCastVO>();

                foreach (ChannelPodCastVO itemChannelVO in listChannelVO)
                {
                    itemChannelVO.ListaItem.Clear();

                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelVO, true).Wait();
                    }, itemChannelVO));
                }
            }

            else if (typeObject.Equals(typeof(ChannelSalvadorVO)))
            {
                DataSourceRadio.ListaTarefaLoadDados.RemoveAll(item =>
                    item.AsyncState.GetType().Equals(typeof(ChannelSalvadorVO)) && arrIdCategoria.Contains(((ChannelSalvadorVO)item.AsyncState).Id)
                );

                List<ChannelSalvadorVO> listChannelVO = DataSourceRadio.ListaChannelSalvadorVO.Where<ChannelSalvadorVO>(item => arrIdCategoria.Contains(item.Id)).ToList<ChannelSalvadorVO>();

                foreach (ChannelSalvadorVO itemChannelVO in listChannelVO)
                {
                    itemChannelVO.ListaItem.Clear();

                    DataSourceRadio.ListaTarefaLoadDados.Add(new Task(delegate
                    {
                        LoadChannelAsync(itemChannelVO, true).Wait();
                    }, itemChannelVO));
                }
            }

        }

        /// <summary>
        /// Método que verifica se as tarefas foram comcluídas.
        /// </summary>
        /// <returns>Retorna uma tarefa do sistema.</returns>
        public static async Task CheckTaskCompleted()
        {
            foreach (Task item in DataSourceRadio.ListaTarefaLoadDados)
            {
                item.Start();
            }

            while (DataSourceRadio.ListaTarefaLoadDados.Where(item => !((Task)item).IsCompleted).Any<object>())
            {
                await Task.Delay(500);

                if (DataSourceRadio.ListaTarefaLoadDados.Where(item => ((Task)item).Status.Equals(TaskStatus.Faulted)).Any<object>())
                {
                    ((MainPage)DataSourceRadio.CurrentPage).Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { object tarefaError = DataSourceRadio.ListaTarefaLoadDados.Where(item => ((Task)item).IsFaulted).First<object>(); ((MainPage)DataSourceRadio.CurrentPage).PopUpMsgError(((Task)tarefaError).Exception.InnerException.InnerException.Message); });
                }
            }

            DataSourceRadio.ChangeChannelActive();
            DataSourceRadio.IsUpdating = false;

            if (DataSourceRadio.CurrentPage.GetType().Equals(typeof(MainPage)))
            {
                ((MainPage)DataSourceRadio.CurrentPage).Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { ((MainPage)DataSourceRadio.CurrentPage).LoadMain(); });
            }
        }

        /// <summary>
        /// Método privado para carregar o channel de filmes em cartaz.
        /// </summary>
        /// <param name="channelVO">Channel de filme em cartaz.</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        private static async Task LoadChannelCinameVOAsync(ChannelCinemaVO channelVO, bool refresh)
        {
            try
            {
                if (Util.IsConnectedToNetwork)
                {
                    await XMLParserVO.LoadXmlUrl(channelVO);
                }
                else if (await Util.ExistsFileAsync(Util.NameFile(channelVO.NomeChannel + channelVO.Id, 0)))
                {
                    await XMLParserVO.LoadXmlLocalAsync(channelVO);
                }
                else
                {
                    throw new Exception("Sem conexão a internet e sem dados!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Método privado para carregar o channel de notícias.
        /// </summary>
        /// <param name="channelVO">Channel de notícias.</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        private static async Task LoadChannelVOAsync(ChannelVO channelVO, bool refresh)
        {
            try
            {
                if (Util.IsConnectedToNetwork)
                {
                    await XMLParserVO.LoadXmlUrl(channelVO);
                }
                else if (await Util.ExistsFileAsync(Util.NameFile(channelVO.NomeChannel, channelVO.Id)))
                {
                    await XMLParserVO.LoadXmlLocalAsync(channelVO);
                }
                else
                {
                    throw new Exception("Sem conexão a internet e sem dados!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Método privado para carregar o channel de Salvador.
        /// </summary>
        /// <param name="channelVO">Channel de Salvador.</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        private static async Task LoadChannelSalvadorVOAsync(ChannelSalvadorVO channelVO, bool refresh)
        {
            try
            {
                if (Util.IsConnectedToNetwork)
                {
                    await XMLParserVO.LoadXmlUrl(channelVO);
                }
                else if (await Util.ExistsFileAsync(Util.NameFile(channelVO.NomeChannel, channelVO.Id)))
                {
                    await XMLParserVO.LoadXmlLocalAsync(channelVO);
                }
                else
                {
                    throw new Exception("Sem conexão a internet e sem dados!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Método privado para carregar o channel de Programacao.
        /// </summary>
        /// <param name="channelVO">Channel de Programacao.</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        private static async Task LoadChannelProgramacaoVOAsync(ChannelProgramacaoVO channelVO, bool refresh)
        {
            try
            {
                if (Util.IsConnectedToNetwork)
                {
                    await XMLParserVO.LoadXmlUrl(channelVO);
                }
                else if (await Util.ExistsFileAsync(Util.NameFile(channelVO.NomeChannel, channelVO.Id)))
                {
                    await XMLParserVO.LoadXmlLocalAsync(channelVO);
                }
                else
                {
                    throw new Exception("Sem conexão a internet e sem dados!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Método privado para carregar o channel de notícias.
        /// </summary>
        /// <param name="channelVO">Channel de notícias.</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        private static async Task LoadChannelCorreioVOAsync(ChannelCorreioVO channelVO, bool refresh)
        {
            try
            {
                if (Util.IsConnectedToNetwork)
                {
                    await XMLParserVO.LoadXmlUrl(channelVO);
                }
                else if (await Util.ExistsFileAsync(Util.NameFile(channelVO.NomeChannel, channelVO.Id)))
                {
                    await XMLParserVO.LoadXmlLocalAsync(channelVO);
                }
                else
                {
                    throw new Exception("Sem conexão a internet e sem dados!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Método privado para carregar o channel de podcast.
        /// </summary>
        /// <param name="channelVO">Channel de podcast.</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        private static async Task LoadChannelPodCastVOAsync(ChannelPodCastVO channelVO, bool refresh)
        {
            try
            {
                if (Util.IsConnectedToNetwork)
                {
                    await XMLParserVO.LoadXmlUrl(channelVO);
                }
                else if (await Util.ExistsFileAsync(Util.NameFile(channelVO.NomeChannel, channelVO.Id)))
                {
                    await XMLParserVO.LoadXmlLocalAsync(channelVO);
                }
                else
                {
                    throw new Exception("Sem conexão a internet e sem dados!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Método privado para carregar o channel de podcast.
        /// </summary>
        /// <param name="channelVO">Channel de podcast.</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        private static async Task LoadChannelPromocaoVOAsync(ChannelPromocaoVO channelVO, bool refresh)
        {
            try
            {
                if (Util.IsConnectedToNetwork)
                {
                    await XMLParserVO.LoadXmlUrl(channelVO);
                }
                else if (await Util.ExistsFileAsync(Util.NameFile(channelVO.NomeChannel, channelVO.Id)))
                {
                    await XMLParserVO.LoadXmlLocalAsync(channelVO);
                }
                else
                {
                    throw new Exception("Sem conexão a internet e sem dados!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        /// <summary>
        /// Método privado para carregar o channel da Agenda Cultural.
        /// </summary>
        /// <param name="channelVO">Channel de Agenda Cultural.</param>
        /// <param name="refresh">Caso for atualizar os dados</param>
        /// <returns>Retorna uma tarefa do sistema</returns>
        private static async Task LoadChannelAgendaCulturalVOAsync(ChannelAgendaCulturalVO channelVO, bool refresh)
        {
            try
            {
                if (Util.IsConnectedToNetwork)
                {
                    await XMLParserVO.LoadXmlUrl(channelVO);
                }
                else if (await Util.ExistsFileAsync(Util.NameFile(channelVO.NomeChannel, channelVO.Id)))
                {
                    await XMLParserVO.LoadXmlLocalAsync(channelVO);
                }
                else
                {
                    throw new Exception("Sem conexão a internet e sem dados!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion
    }
}