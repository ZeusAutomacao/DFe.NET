using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTe.Classes.Simplificado.Carga
{
    /// <summary>
    /// Informações da carga do CT-e.
    /// </summary>
    public class infCarga
    {
        /// <summary>
        /// Valor total da carga.
        /// </summary>
        [XmlElement(ElementName = "vCarga")]
        public decimal vCarga { get; set; }

        /// <summary>
        /// Produto predominante.
        /// </summary>
        [XmlElement(ElementName = "proPred")]
        public string proPred { get; set; }

        /// <summary>
        /// Detalhamento dos itens da carga.
        /// </summary>
        [XmlElement(ElementName = "det")]
        public List<det> det { get; set; }
    }
}
