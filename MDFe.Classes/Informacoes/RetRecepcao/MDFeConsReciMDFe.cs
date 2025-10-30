using System;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Classes.Informacoes.RetRecepcao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "consReciMDFe")]
    public class MDFeConsReciMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }
        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "nRec")]
        public string NRec { get; set; }
    }
}