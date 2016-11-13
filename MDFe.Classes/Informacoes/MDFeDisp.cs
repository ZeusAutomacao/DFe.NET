using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeDisp
    {
        [XmlElement(ElementName = "CNPJForn")]
        public string CNPJForn { get; set; }

        [XmlElement(ElementName = "CNPJPg")]
        public string CNPJPg { get; set; }

        [XmlElement(ElementName = "nCompra")]
        public string NCompra { get; set; }


    }
}