using DFe.Utils;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    /// <summary>
    /// Resumo de CT-e retornado no docZip com schema resCTe_v1.03.xsd
    /// Ref: NT 2015/002 - CTeDistribuicaoDFe
    /// </summary>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/cte")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte", IsNullable = false)]
    public class resCTe
    {
        /// <summary>
        /// Chave de acesso do CT-e (44 dígitos)
        /// </summary>
        [XmlElement("chCTe")]
        public string chCTe { get; set; }

        /// <summary>
        /// CNPJ do emitente (14 dígitos)
        /// </summary>
        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        /// <summary>
        /// Razão social ou nome do emitente
        /// </summary>
        [XmlElement("xNome")]
        public string xNome { get; set; }

        /// <summary>
        /// Inscrição Estadual do emitente
        /// </summary>
        [XmlElement("IE")]
        public string IE { get; set; }

        /// <summary>
        /// Modal do CT-e
        /// <list>
        /// <item>01=Rodoviário</item>
        /// <item>02=Aéreo</item>
        /// <item>03=Aquaviário</item>
        /// <item>04=Ferroviário</item>
        /// <item>05=Dutoviário</item>
        /// <item>06=Multimodal</item>
        /// </list>
        /// </summary>
        [XmlElement("modal")]
        public string modal { get; set; }

        /// <summary>
        /// Data e hora de emissão do CT-e
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhEmi { get; set; }

        [XmlElement(ElementName = "dhEmi")]
        public string ProxydhEmi
        {
            get { return dhEmi.ParaDataHoraStringUtc(); }
            set { dhEmi = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// Tipo do CT-e
        /// <list>
        /// <item>0=CT-e Normal</item>
        /// <item>1=CT-e de Complemento de Valores</item>
        /// <item>2=CT-e de Anulação</item>
        /// <item>3=CT-e de Substituição</item>
        /// </list>
        /// </summary>
        [XmlElement("tpCTe")]
        public string tpCTe { get; set; }

        /// <summary>
        /// Digest value da assinatura do CT-e
        /// </summary>
        [XmlElement("digVal")]
        public string digVal { get; set; }

        /// <summary>
        /// Data e hora do recebimento pelo Ambiente Nacional
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxydhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// Número do protocolo de autorização
        /// </summary>
        [XmlElement("nProt")]
        public string nProt { get; set; }

        /// <summary>
        /// Valor a receber pelo transportador
        /// </summary>
        [XmlElement("vRec")]
        public string vRec { get; set; }

        /// <summary>
        /// Situação do CT-e
        /// <list>
        /// <item>100=Autorizado o uso do CT-e</item>
        /// <item>101=Cancelamento de CT-e homologado</item>
        /// <item>110=Uso denegado</item>
        /// </list>
        /// </summary>
        [XmlElement("cSitCTe")]
        public string cSitCTe { get; set; }
    }
}
