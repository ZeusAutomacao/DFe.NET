using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeEmit
    {
        public MDFeEmit()
        {
            EnderEmit = new MDFeEnderEmit();
        }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "IE")]
        public string IE { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        [XmlElement(ElementName = "xFant")]
        public string XFant { get; set; }

        [XmlElement(ElementName = "enderEmit")]
        public MDFeEnderEmit EnderEmit { get; set; }
    }

    [Serializable]
    public class MDFeEnderEmit
    {
        [XmlElement(ElementName = "xLgr")]
        public string XLgr { get; set; }

        [XmlElement(ElementName = "nro")]
        public string Nro { get; set; }

        [XmlElement(ElementName = "xCpl")]
        public string XCpl { get; set; }

        [XmlElement(ElementName = "xBairro")]
        public string XBairro { get; set; }

        [XmlElement(ElementName = "cMun")]
        public long CMun { get; set; }

        [XmlElement(ElementName = "xMun")]
        public string XMun { get; set; }

        [XmlIgnore]
        public long CEP { get; set; }

        [XmlElement(ElementName = "CEP")]
        public string ProxyCEP
        {
            get { return CEP.ToString("D8"); }
            set { CEP = long.Parse(value); }
        }

        [XmlIgnore]
        public EstadoUF UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }

        [XmlElement(ElementName = "fone")]
        public string Fone { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
    }
}