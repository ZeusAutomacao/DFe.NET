using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Contratos;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeTrem : MDFeModalContainer
    {
        [XmlElement(ElementName = "xPref")]
        public string XPref { get; set; }

        [XmlIgnore]
        public DateTime? DhTrem { get; set; }

        [XmlElement(ElementName = "dhTrem")]
        public string ProxyDhTrem {
            get { return DhTrem?.ToString("yyyy-MM-ddTHH:mm:dd"); }
            set { DhTrem = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "xOri")]
        public string XOri { get; set; }

        [XmlElement(ElementName = "xDest")]
        public string XDest { get; set; }

        [XmlElement(ElementName = "qVag")]
        public int QVag { get; set; }

        public List<MDFeVag> Vag { get; set; }
    }
}