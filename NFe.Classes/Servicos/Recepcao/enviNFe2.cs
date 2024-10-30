using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.Recepcao
{
    [XmlRoot(ElementName = "enviNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class enviNFe2
    {
        public enviNFe2(string versao, int idLote, List<NFe> nFe)
        {
            this.versao = versao;
            this.idLote = idLote;
            NFe = nFe;
        }

        internal enviNFe2() //para serialização apenas
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
        ///     AP04 - Conjunto de NF-e transmitidas
        /// </summary>
        [XmlElement("NFe")]
        public List<NFe> NFe { get; set; }
    }
}