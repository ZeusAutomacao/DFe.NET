using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;
using MDFe.Classes.Informacoes.StatusServico;

namespace MDFe.Classes.Retorno.MDFeStatusServico
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "retConsStatServMDFe")]
    public class MDFeRetConsStatServ : RetornoBase
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VerAplic { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CStat { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string XMotivo { get; set; }

        [XmlIgnore]
        public Estado CUF { get; set; }

        [XmlElement(ElementName = "cUF")]
        public string CUFProxy
        {
            get
            {
                return CUF.GetCodigoIbgeEmString();
            }
            set { CUF = CUF.CodigoIbgeParaEstado(value); }
        }

        [XmlElement(ElementName = "dhRecbto")]
        public DateTime DhRecbto { get; set; }

        [XmlElement(ElementName = "tMed")]
        public int? TMed { get; set; }

        [XmlElement(ElementName = "dhRetorno")]
        public DateTime? DhRetorno { get; set; }

        [XmlElement(ElementName = "xObs")]
        public string XObs { get; set; }

        public bool TMedSpecified { get { return TMed.HasValue; } }
        public bool DhRetornoSpecified { get { return DhRetorno.HasValue; } }

        public static MDFeRetConsStatServ LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetConsStatServ>(xml);
            retorno.RetornoXmlString = xml;

            return retorno;
        }

        public static MDFeRetConsStatServ LoadXml(string xml, MDFeConsStatServMDFe consStatServMdFe)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(consStatServMdFe);

            return retorno;
        }
    }
}