using System.Collections.Generic;
using System.Xml.Serialization;
using CteEletronica = CTe.Classes.CTe;

namespace CTeDLL.Classes.Servicos.Recepcao
{
    [XmlRoot(ElementName = "enviCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class enviCTe
    {
        public enviCTe(string versao, int idLote, List<CteEletronica> cTe)
        {
            this.versao = versao;
            this.idLote = idLote;
            CTe = cTe;
        }

        internal enviCTe() //para serialização apenas
        {
        }

        /// <summary>
        ///     AP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     AP03 - Identificador de controle do envio do lote.
        /// </summary>
        public int idLote { get; set; }

        /// <summary>
        ///     AP04 - Conjunto de CT-e transmitidas
        /// </summary>
        [XmlElement("CTe")]
        public List<CTe.Classes.CTe> CTe { get; set; }
    }
}