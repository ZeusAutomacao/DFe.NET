using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Retorno
{
    [Serializable]
    [XmlRoot(ElementName = "procEventoMDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class MDFeProcEventoMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }
    }
}