using CTe.Classes.Protocolo;
using CTe.Classes.Simplificado;
using DFe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CTe.Classes
{
    /// <summary>
    /// CT-e Simplificado processado
    /// </summary>
    [XmlRoot("cteSimpProc", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class cteSimpProc
    {
        /// <summary>
        /// Representa o CT-e Simplificado
        /// </summary>
        [XmlElement("CTeSimp")]
        public cteSimp cteSimp { get; set; }

        /// <summary>
        /// Representa o protocolo do CT-e
        /// </summary>
        [XmlElement("protCTe")]
        public protCTe protCTe { get; set; }

        /// <summary>
        /// Versão do CT-e
        /// </summary>
        [XmlAttribute("versao")]
        public string versao { get; set; }

        /// <summary>
        /// IP do transmissor do documento fiscal para o ambiente autorizador
        /// </summary>
        [XmlAttribute("ipTransmissor")]
        public string ipTransmissor { get; set; }

        /// <summary>
        /// Porta de origem utilizada na conexão (De 0 a 65535)
        /// </summary>
        [XmlAttribute("nPortaCon")]
        public string nPortaCon { get; set; }

        
        [XmlIgnore]
        public DateTimeOffset? dhConexao { get; set; }

        /// <summary>
        /// Data e Hora da Conexão de Origem
        /// </summary>
        [XmlElement(ElementName = "dhConexao")]
        public string DhConexao
        {
            get { return dhConexao.ParaDataHoraStringUtc(); }
            set { dhConexao = DateTimeOffset.Parse(value); }
        }

        public static cteSimpProc LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<cteSimpProc>(xml);
        }

        public static cteSimpProc LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<cteSimpProc>(caminhoArquivoXml);
        }
    }
}
