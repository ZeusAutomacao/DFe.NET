using System.Collections.Generic;
using System.Xml.Serialization;
using MDFe.Classes.Contratos;

namespace MDFe.Classes.Informacoes
{
    public class MDFeFerrov : MDFeModalContainer
    {
        /// <summary>
        /// 1 - Informações da composição do trem
        /// </summary>
        [XmlElement(ElementName = "trem")]
        public MDFeTrem Trem { get; set; }

        /// <summary>
        /// 1 - informações dos Vagões
        /// </summary>
        [XmlElement(ElementName = "vag")]
        public List<MDFeVag> Vag { get; set; }
    }
}