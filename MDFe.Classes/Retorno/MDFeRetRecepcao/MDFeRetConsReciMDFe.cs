using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;

namespace ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeRetRecepcao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "retConsReciMDFe")]
    public class MDFeRetConsReciMDFe
    {
        [XmlElement(ElementName = "versao")]
        public MDFeVersaoServico Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VerAplic { get; set; }

        [XmlElement(ElementName = "nRec")]
        public string NRec { get; set; }

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

        [XmlElement(ElementName = "protMDFe")]
        public MDFeProtMDFe ProtMdFe { get; set; }
    }
}