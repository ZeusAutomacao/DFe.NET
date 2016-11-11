using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    public class MDFeIde
    {
        [XmlElement(ElementName = "cUF")]
        public EstadoUF CUF { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "tpEmit")]
        public MDFeTipoEmitente TpEmit { get; set; }

        [XmlElement(ElementName = "mod")]
        public MDFeModelo Mod { get; set; }

        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        [XmlElement(ElementName = "nMDF")]
        public long NMDF { get; set; }

        [XmlElement(ElementName = "cMDF")]
        public long CMDF { get; set; }

        [XmlElement(ElementName = "cDV")]
        public byte CDV { get; set; }

        [XmlElement(ElementName = "modal")]
        public MDFeModal Modal { get; set; }

        [XmlIgnore]
        public DateTime DhEmi { get; set; }

        public string ProxyDhEmi
        {
            get { return DhEmi.ToString("yyyy-MM-ddTHH:mm:dd"); }
            set { DhEmi = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "tpEmis")]
        public MDFeTipoEmissao TpEmis { get; set; }

        [XmlElement(ElementName = "procEmi")]
        public MDFeIdentificacaoProcessoEmissao ProcEmi { get; set; }

        [XmlElement(ElementName = "verProc")]
        public string VerProc { get; set; }

        [XmlIgnore]
        public EstadoUF UFIni { get; set; }

        [XmlElement(ElementName = "UFIni")]
        public string ProxyUFIni
        {
            get { return UFIni.ToString(); }
            set { UFIni = UFIni.SiglaParaEstado(value); }
        }

        [XmlIgnore]
        public EstadoUF UFFim { get; set; }

        [XmlElement(ElementName = "UFFim")]
        public string ProxyUFFim
        {
            get { return UFFim.ToString(); }
            set { UFFim = UFFim.SiglaParaEstado(value); }
        }
    }
}