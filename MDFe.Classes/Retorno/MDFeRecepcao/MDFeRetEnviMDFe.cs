using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;
using MDFe.Classes.Servicos.Autorizacao;

namespace MDFe.Classes.Retorno.MDFeRecepcao
{
    [Serializable]
    [XmlRoot(ElementName = "retEnviMDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class MDFeRetEnviMDFe : RetornoBase
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

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

        [XmlElement(ElementName = "verAplic")]
        public string VerAplic { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CStat { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string XMotivo { get; set; }

        [XmlElement(ElementName = "infRec")]
        public MDFeInfRec InfRec { get; set; }

        [XmlIgnore]
        public string RetornoCompleto { get; set; }

        public static MDFeRetEnviMDFe LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetEnviMDFe>(xml);
            retorno.RetornoXmlString = xml;

            return retorno;
        }

        public static MDFeRetEnviMDFe LoadXml(string xml, MDFeEnviMDFe enviMDFe)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(enviMDFe.MDFe);
            retorno.RetornoCompleto = FuncoesXml.ClasseParaXmlString(enviMDFe);

            return retorno;
        }
    }

    [Serializable]
    public class MDFeInfRec
    {
        [XmlElement(ElementName = "nRec")]
        public string NRec { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public DateTime DhRecbto { get; set; }

        [XmlElement(ElementName = "tMed")]
        public int TMed { get; set; }
    }
}