﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.ValueObject.Xml.AgendaCulturalXML.Cinema.Cartaz
{
    /// <summary>
    /// Classe Value Object Channel
    /// </summary>
    [KnownType(typeof(ChannelCinemaVO))]
    [DataContractAttribute]
    public class ChannelCinemaVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto está carregado
        /// </summary>
        private bool isLoaded;

        /// <summary>
        /// Atríbuto nomeChannel. 
        /// </summary>
        private string nomeChannel;

        /// <summary>
        /// Atributo IsActive
        /// </summary>
        private bool isActive = false;

        /// <summary>
        /// Atríbuto id. 
        /// </summary>
        private string id;

        /// <summary>
        /// Atríbuto URL. 
        /// </summary>
        private string url;

        /// <summary>
        /// Atríbuto title. 
        /// </summary>
        private string title;

        /// <summary>
        /// Atríbuto description. 
        /// </summary>
        private string description;

        /// <summary>
        /// Atríbuto link. 
        /// </summary>
        private string link;

        /// <summary>
        /// Atríbuto lastBuildDate.
        /// </summary>
        private string lastBuildDate;

        /// <summary>
        /// Atríbuto pubDate.
        /// </summary>
        private string pubDate;

        /// <summary>
        /// Atríbuto listaItem. 
        /// </summary>
        private ObservableCollection<ItemCinemaVO> listaItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelCinemaVO"/> class.
        /// </summary>
        public ChannelCinemaVO()
        {
            this.isLoaded = false;
            this.listaItem = new ObservableCollection<ItemCinemaVO>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsLoaded
        /// </summary>
        public bool IsLoaded
        {
            get { return this.isLoaded; }
            set { this.SetProperty(ref this.isLoaded, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade NomeChannel.
        /// </summary>
        public string NomeChannel
        {
            get { return this.nomeChannel; }
            set { this.SetProperty(ref this.nomeChannel, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Id.
        /// </summary>
        public string Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Url.
        /// </summary>
        public string Url
        {
            get { return this.url; }
            set { this.SetProperty(ref this.url, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Description.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade Link.
        /// </summary>
        public string Link
        {
            get { return this.link; }
            set { this.SetProperty(ref this.link, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade LastBuildDate.
        /// </summary>
        public string LastBuildDate
        {
            get { return this.lastBuildDate; }
            set { this.SetProperty(ref this.lastBuildDate, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade PubDate.
        /// </summary>
        public string PubDate
        {
            get { return this.pubDate; }
            set { this.SetProperty(ref this.pubDate, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaItem.
        /// </summary>
        public ObservableCollection<ItemCinemaVO> ListaItem
        {
            get { return this.listaItem; }
            set { this.SetProperty(ref this.listaItem, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive.
        /// </summary>
        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }
    }
}
