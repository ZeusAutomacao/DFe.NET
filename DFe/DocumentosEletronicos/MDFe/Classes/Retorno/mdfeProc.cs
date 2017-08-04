using System;
using System.Xml.Serialization;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Retorno
{
    [Serializable]
    [XmlRoot(ElementName = "mdfeProc", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class mdfeProc
    {
        public mdfeProc()
        {
            versao = VersaoServico.Versao100;
        }

        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico versao { get; set; }

        [XmlElement(ElementName = "MDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        public Informacoes.MDFe MDFe { get; set; }

        [XmlElement(ElementName = "protMDFe")]
        public protMDFe protMDFe { get; set; }
    }
}