using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeIde
    {
        public MDFeIde()
        {
            InfMunCarrega = new List<MDFeInfMunCarrega>();
            //InfPercursos = new List<MDFeInfPercurso>();
        }

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

        [XmlElement(ElementName = "dhEmi")]
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
            get { return UFIni.GetSiglaUfString(); }
            set { UFIni = UFIni.SiglaParaEstado(value); }
        }

        [XmlIgnore]
        public EstadoUF UFFim { get; set; }

        [XmlElement(ElementName = "UFFim")]
        public string ProxyUFFim
        {
            get { return UFFim.GetSiglaUfString(); }
            set { UFFim = UFFim.SiglaParaEstado(value); }
        }

        [XmlElement(ElementName = "infMunCarrega")]
        public List<MDFeInfMunCarrega> InfMunCarrega { get; set; }

        /*[XmlElement(ElementName = "infPercurso")]
        public List<MDFeInfPercurso> InfPercursos { get; set; }*/

        [XmlIgnore]
        public DateTime? DhIniViagem { get; set; }

        [XmlElement(ElementName = "dhIniViagem")]
        public string ProxyDhIniViagem {
            get { return DhIniViagem?.ToString("yyyy-MM-ddTHH:mm:dd"); }
            set { DhIniViagem = DateTime.Parse(value); }
        }
    }
}