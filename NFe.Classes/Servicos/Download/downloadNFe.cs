using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Flags;

namespace NFe.Classes.Servicos.Download
{
    /// <summary>
    /// JP01 - Estrutura com o pedido de download de NF-e 
    /// </summary>
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class downloadNFe
    {
        public downloadNFe()
        {
            xServ = "DOWNLOAD NFE";
        }

        /// <summary>
        ///     JP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        /// JP03 - Identificação do Ambiente: 1=Produção/2=Homologação 
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        /// JP04 - Serviço Solicitado ‘DOWNLOAD NFE’ 
        /// </summary>
        public string xServ { get; set; }

        /// <summary>
        /// JP05 - CNPJ do destinatário da NF-e 
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// JP06 - Chave de Acesso da NF-e 
        /// </summary>
        [XmlElement("chNFe")]
        public List<string> chNFe { get; set; }
    }
}