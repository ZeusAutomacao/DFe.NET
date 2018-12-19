using System;
using System.Xml.Serialization;
using SMDFe.Classes.Retorno.MDFeRetRecepcao;
using SMDFe.Utils.Flags;

namespace SMDFe.Classes.Retorno
{
    [Serializable]
    [XmlRoot(ElementName = "mdfeProc", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class MDFeProcMDFe
    {
        public MDFeProcMDFe()
        {
            Versao = VersaoServico.Versao100;
        }

        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "MDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        public Informacoes.MDFe MDFe { get; set; }

        [XmlElement(ElementName = "protMDFe")]
        public MDFeProtMDFe ProtMDFe { get; set; }
    }
}