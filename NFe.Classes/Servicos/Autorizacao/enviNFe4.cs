using System.Collections.Generic;
using System.Xml.Serialization;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Classes.Servicos.Autorizacao
{
    [XmlRoot(ElementName = "enviNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class enviNFe4
    {
        public enviNFe4(string versao, int idLote, IndicadorSincronizacao indSinc, List<NFe> nFe)
        {
            this.versao = versao;
            this.idLote = idLote;
            this.indSinc = indSinc;
            NFe = nFe;
        }

        internal enviNFe4() //para serialização apenas
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
        ///     AP03a - Indicador de Sincronização
        /// </summary>
        public IndicadorSincronizacao indSinc { get; set; }

        /// <summary>
        ///     AP04 - Conjunto de NF-e transmitidas
        /// </summary>
        [XmlElement("NFe")]
        public List<NFe> NFe { get; set; }
    }
}