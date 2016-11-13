using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeProp
    {

        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "RNTRC")]
        public string RNTRC { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        [XmlElement(ElementName = "IE")]
        public string IE { get; set; }

        [XmlIgnore]
        public EstadoUF UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get
            {
                return UF.GetSiglaUfString();
            }
            set { UF = UF.SiglaParaEstado(value); }
        }

        [XmlElement(ElementName = "tpProp")]
        public MDFeTpProp MDFeTpProp { get; set; }
    }
}