using System;
using System.Xml.Serialization;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.Classes.Servicos.Status
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retConsStatServCte : RetornoBase
    {
        /// <summary>
        ///     FR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     FR03 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     FR04 - Versão do Aplicativo que processou a consulta. A versão deve ser iniciada com a sigla da UF nos casos de WS
        ///     próprio ou a sigla SCAN, SVAN ou SVRS nos demais casos.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     FR05 - Código do status da resposta.
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     FR06 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     FR07 - Código da UF que atendeu a solicitação
        /// </summary>
        public Estado cUF { get; set; }

        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxydhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        public int tMed { get; set; }

        [XmlIgnore]
        public DateTime dhRetorno { get; set; }

        [XmlElement(ElementName = "dhRetorno")]
        public string ProxydhRetorno
        {
            get { return dhRetorno.ParaDataHoraString(); }
            set { dhRetorno = DateTime.Parse(value); }
        }

        public string xObs { get; set; }

        public static retConsStatServCte LoadXml(string xml, consStatServCte consStatServCte)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(consStatServCte);

            return retorno;
        }

        private static retConsStatServCte LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retConsStatServCte>(xml);
            retorno.RetornoXmlString = xml;

            return retorno;
        }
    }

    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retConsStatServCTe : RetornoBase
    {
        /// <summary>
        ///     FR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     FR03 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     FR04 - Versão do Aplicativo que processou a consulta. A versão deve ser iniciada com a sigla da UF nos casos de WS
        ///     próprio ou a sigla SCAN, SVAN ou SVRS nos demais casos.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     FR05 - Código do status da resposta.
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     FR06 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     FR07 - Código da UF que atendeu a solicitação
        /// </summary>
        public Estado cUF { get; set; }

        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxydhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        public int tMed { get; set; }

        [XmlIgnore]
        public DateTime dhRetorno { get; set; }

        [XmlElement(ElementName = "dhRetorno")]
        public string ProxydhRetorno
        {
            get { return dhRetorno.ParaDataHoraString(); }
            set { dhRetorno = DateTime.Parse(value); }
        }

        public string xObs { get; set; }

        public static retConsStatServCTe LoadXml(string xml, consStatServCTe consStatServCte)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(consStatServCte);

            return retorno;
        }

        private static retConsStatServCTe LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retConsStatServCTe>(xml);
            retorno.RetornoXmlString = xml;

            return retorno;
        }
    }
}