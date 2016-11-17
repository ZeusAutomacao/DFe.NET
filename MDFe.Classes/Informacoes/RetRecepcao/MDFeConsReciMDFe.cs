using System;
using System.Xml.Serialization;
using DFe.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.RetRecepcao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "consReciMDFe")]
    public class MDFeConsReciMDFe
    {
        [XmlElement(ElementName = "versao")]
        public MDFeVersaoServico Versao { get; set; } = MDFeVersaoServico.Versao100;

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "nRec")]
        public string NRec { get; set; }
    }
}