using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.ManipuladorDeXml;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Servicos.Autorizacao
{
    [XmlRoot(ElementName = "enviCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class enviCTeOs
    {
        public enviCTeOs(VersaoServico versao, int idLote, List<CTe.CTeOS.CTeOS> cte)
        {
            this.versao = versao;
            this.idLote = idLote;
            CTeOs = cte;
        }

        internal enviCTeOs() //para serialização apenas
        {
        }

        /// <summary>
        ///     AP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public VersaoServico versao { get; set; }

        /// <summary>
        ///     AP03 - Identificador de controle do envio do lote.
        /// </summary>
        public int idLote { get; set; }

        /// <summary>
        ///     AP04 - Conjunto de CT-e OS transmitidas
        /// </summary>
        [XmlElement(ElementName = "CTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
        public List<CTeOS> CTeOs { get; set; }


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