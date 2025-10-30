using System;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Classes.Informacoes.StatusServico
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "consStatServMDFe")]
    public class MDFeConsStatServMDFe
    {
        public MDFeConsStatServMDFe()
        {
            XServ = "STATUS";
        }

        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string XServ { get; set; }
    }
}