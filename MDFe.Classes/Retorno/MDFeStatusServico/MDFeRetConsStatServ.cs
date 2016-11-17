using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeStatusServico
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "retConsStatServMDFe")]
    public class MDFeRetConsStatServ
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

        [XmlElement(ElementName = "dhRecbto")]
        public DateTime DhRecbto { get; set; }

        [XmlElement(ElementName = "tMed")]
        public int? TMed { get; set; }

        [XmlElement(ElementName = "dhRetorno")]
        public DateTime? DhRetorno { get; set; }

        [XmlElement(ElementName = "xObs")]
        public string XObs { get; set; }

        public bool TMedSpecified => TMed.HasValue;
        public bool DhRetornoSpecified => DhRetorno.HasValue;
    }
}