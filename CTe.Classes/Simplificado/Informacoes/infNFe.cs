using System;
using System.Xml.Serialization;

namespace CTe.Classes.Simplificado.Informacoes
{
    /// <summary>
    /// Informações da NF-e vinculada à entrega.
    /// </summary>
    public class infNFe
    {
        /// <summary>
        /// Chave de acesso da NF-e.
        /// </summary>
        [XmlElement(ElementName = "chNFe")]
        public string chNFe { get; set; }

        /// <summary>
        /// Data prevista de entrega.
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dPrev { get; set; }

        /// <summary>
        /// Data prevista de entrega (formato AAAA-MM-DD).
        /// </summary>
        [XmlElement(ElementName = "dPrev")]
        public string DPrev
        {
            get { return dPrev?.ToString("yyyy-MM-dd"); }
            set { dPrev = DateTimeOffset.Parse(value); }
        }
    }
}
