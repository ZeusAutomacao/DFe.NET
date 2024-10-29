using System.Xml.Serialization;
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    public class MDFeInfUnidCargaVazia
    {
        /// <summary>
        /// 2 - Identificação da unidades de carga vazia
        /// </summary>
        [XmlElement(ElementName = "idUnidCargaVazia")]
        public string IdUnidCargaVazia { get; set; }

        /// <summary>
        /// 2 - Tipo da unidade de carga vazia 
        /// </summary>
        [XmlElement(ElementName = "tpUnidCargaVazia")]
        public MDFeTpUnidCargaVazia TpUnidCargaVazia { get; set; }
    }
}