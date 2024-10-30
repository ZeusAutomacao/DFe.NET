using System;
using System.ComponentModel;
using System.Xml.Serialization;
using DFe.Utils;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class resNFe
    {
        /// <summary>
        /// C02 - Versão do leiaute
        /// </summary>
        [XmlAttribute()]
        public decimal versao { get; set; }

        /// <summary>
        /// C03 - Chave de acesso da NF-e
        /// </summary>
        [XmlElement(DataType = "integer")]
        public string chNFe { get; set; }

        /// <summary>
        /// C04 - CNPJ do Emitente
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// C05 - CPF do Emitente
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// C06 - Razão Social ou Nome do Emitente
        /// </summary>
        public string xNome { get; set; }

        /// <summary>
        /// C07 - IE do Emitente. Valores válidos: 
        /// vazio  (não contribuinte do ICMS), 
        /// ISENTO (contribuinte do ICMS ISENTO de Inscrição no Cadastro de Contribuintes) ou 
        /// IE (Contribuinte do ICMS)
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        /// C08 - Data de Emissão da NF-e
        /// </summary>
        [XmlIgnore]
        public DateTime dhEmi { get; set; }

        /// <summary>
        /// Proxy para dhEmi no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhEmi")]
        public string ProxyDhEmi
        {
            get { return dhEmi.ParaDataHoraStringUtc(); }
            set { dhEmi = DateTime.Parse(value); }
        }

        /// <summary>
        /// C09 - Tipo de Operação da NF-e: 0=Entrada; 1=Saída
        /// </summary>
        public byte tpNF { get; set; }

        /// <summary>
        /// C10 - Valor Total da NF-e
        /// </summary>
        public decimal vNF { get; set; }

        /// <summary>
        /// C11 - Digest Value da NF-e na base de dados do Ambiente Nacional
        /// </summary>
        public string digVal { get; set; }

        /// <summary>
        /// C12 - Data de autorização da NF-e
        /// </summary>
        public DateTime dhRecbto { get; set; }

        /// <summary>
        /// C13 - Número de protocolo da NF-e
        /// </summary>
        public ulong nProt { get; set; }

        /// <summary>
        /// C14 - Situação da NF-e: 1=Uso autorizado; 2=Uso denegado.
        /// </summary>
        public byte cSitNFe { get; set; }
    }
}