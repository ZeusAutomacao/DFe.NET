using System;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Classes.Informacoes.ConsultaProtocolo
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "consSitMDFe")]
    public class MDFeConsSitMDFe
    {
        public MDFeConsSitMDFe()
        {
            XServ = "CONSULTAR";
        }

        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string XServ { get; set; }

        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }
    }
}