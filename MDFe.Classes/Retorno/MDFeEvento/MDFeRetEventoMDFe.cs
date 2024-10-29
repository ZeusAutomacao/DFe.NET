using System;
using System.Xml.Serialization;
using DFe.Classes.Assinatura;
using DFe.Utils;
using MDFe.Classes.Informacoes.Evento;

namespace MDFe.Classes.Retorno.MDFeEvento
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "retEventoMDFe")]
    public class MDFeRetEventoMDFe : RetornoBase
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infEvento")]
        public MDFeRetInfEvento InfEvento { get; set; }

        [XmlElement(ElementName = "Signature")]
        public Signature Signature { get; set; }

        public static MDFeRetEventoMDFe LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetEventoMDFe>(xml);
            retorno.RetornoXmlString = xml;

            return retorno;
        }

        public static MDFeRetEventoMDFe LoadXml(string xml, MDFeEventoMDFe evento)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(evento);

            return retorno;
        }
    }
}