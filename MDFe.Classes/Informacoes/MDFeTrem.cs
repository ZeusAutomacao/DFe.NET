using System;
using System.Xml.Serialization;
using DFe.Utils;
using MDFe.Classes.Contratos;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeTrem : MDFeModalContainer
    {
        /// <summary>
        /// 2 - Prefixo do Trem 
        /// </summary>
        [XmlElement(ElementName = "xPref")]
        public string XPref { get; set; }

        /// <summary>
        /// 2 - Data e hora de liberação do trem na origem
        /// </summary>
        [XmlIgnore]
        public DateTime? DhTrem { get; set; }

        /// <summary>
        /// Proxy para covnerter dhTrem em string yyyy-MM-ddTHH:mm:dd
        /// </summary>
        [XmlElement(ElementName = "dhTrem")]
        public string ProxyDhTrem {
            get { return DhTrem.ParaDataHoraStringUtc(); }
            set { DhTrem = DateTime.Parse(value); }
        }

        /// <summary>
        /// 2 - Origem do Trem 
        /// </summary>
        [XmlElement(ElementName = "xOri")]
        public string XOri { get; set; }

        /// <summary>
        /// 2 - Destino do Trem 
        /// </summary>
        [XmlElement(ElementName = "xDest")]
        public string XDest { get; set; }

        /// <summary>
        /// 2 - Quantidade de vagões carregados 
        /// </summary>
        [XmlElement(ElementName = "qVag")]
        public short QVag { get; set; }
    }
}