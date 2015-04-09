using RadioPlayer.Beans.ValueObject.Xml.Salvador;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Beans.ValueObject.Xml.ProgramacaoRadioXML
{

    /// <summary>
    /// Classe Value Object ItemProgramacaoRadioXMLVO.
    /// </summary>
    [KnownType(typeof(ItemProgramacaoRadioXMLVO))]
    [DataContractAttribute]
    public class ItemProgramacaoRadioXMLVO : BindableBaseVO
    {
        /// <summary>
        /// Atríbuto title.
        /// </summary>
        private string title;

        /// <summary>
        /// Atríbuto description.
        /// </summary>
        private string description;
        /// <summary>
        /// Atríbuto guid.
        /// </summary>
        private string guid;

          /// <summary>
        /// Atríbuto listaCategoria.
        /// </summary>
        private ObservableCollection<CategoriaProgramacaoRadioXMLVO> listaCategoria;

        /// <summary>
        /// Atríbuto listaHorarioProgramacaoRadioXMLVO.
        /// </summary>
        private ObservableCollection<HorarioProgramacaoRadioXMLVO> listaHorarioProgramacaoRadioXMLVO;

        /// <summary>
        /// Atríbuto listaImage.
        /// </summary>
        private ObservableCollection<ImageChannelProgramacaoRadioXMLVO> listaImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPodCastVO"/> class.
        /// </summary>
        public ItemProgramacaoRadioXMLVO()
        {
            this.listaCategoria = new ObservableCollection<CategoriaProgramacaoRadioXMLVO>();
            this.listaImage = new ObservableCollection<ImageChannelProgramacaoRadioXMLVO>();
            this.listaHorarioProgramacaoRadioXMLVO = new ObservableCollection<HorarioProgramacaoRadioXMLVO>();
        }

        /// <summary>
        /// Gets or sets - propriedade title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade description.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }


        /// <summary>
        /// Gets or sets - propriedade guid.
        /// </summary>
        public string Guid
        {
            get { return this.guid; }
            set { this.SetProperty(ref this.guid, value); }
        }


        /// <summary>
        /// Gets or sets - propriedade ListaCategoria.
        /// </summary>
        public ObservableCollection<CategoriaProgramacaoRadioXMLVO> ListaCategoria
        {
            get { return this.listaCategoria; }
            set { this.SetProperty(ref this.listaCategoria, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaImage.
        /// </summary>
        public ObservableCollection<ImageChannelProgramacaoRadioXMLVO> ListaImage
        {
            get { return this.listaImage; }
            set { this.SetProperty(ref this.listaImage, value); }
        }

        /// <summary>
        /// Gets or sets - propriedade ListaHorarioProgramacaoRadioXMLVO.
        /// </summary>
        public ObservableCollection<HorarioProgramacaoRadioXMLVO> ListaHorarioProgramacaoRadioXMLVO
        {
            get { return this.listaHorarioProgramacaoRadioXMLVO; }
            set { this.SetProperty(ref this.listaHorarioProgramacaoRadioXMLVO, value); }
        }
    }
}
