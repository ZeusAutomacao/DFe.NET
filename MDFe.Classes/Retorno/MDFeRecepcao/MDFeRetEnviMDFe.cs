using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeRecepcao
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
        public EstadoUF CUF { get; set; }

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