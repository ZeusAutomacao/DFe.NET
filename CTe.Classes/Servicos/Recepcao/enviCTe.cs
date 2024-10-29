using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Servicos.Tipos;
using DFe.Utils;
using CteEletronica = CTe.Classes.CTe;

namespace CTe.Classes.Servicos.Recepcao
{
    [XmlRoot(ElementName = "enviCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class enviCTe
    {
        public enviCTe(versao versao, int idLote, List<CteEletronica> cTe)
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
        public versao versao { get; set; }

        /// <summary>
        ///     AP03 - Identificador de controle do envio do lote.
        /// </summary>
        public int idLote { get; set; }

        /// <summary>
        ///     AP04 - Conjunto de CT-e transmitidas
        /// </summary>
        [XmlElement(ElementName = "CTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
        public List<CteEletronica> CTe { get; set; }


        public static enviCTe LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<enviCTe>(xml);
        }

        public static enviCTe LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<enviCTe>(caminhoArquivoXml);
        }
    }
}