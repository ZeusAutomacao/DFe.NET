using System;
using System.Xml.Serialization;
using DFe.Utils;
using MDFe.Utils.Flags;

namespace MDFe.Classes.Servicos.Autorizacao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "enviMDFe")]
    public class MDFeEnviMDFe
    {
        public MDFeEnviMDFe()
        {
            Versao = VersaoServico.Versao100;
        }

        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "idLote")]
        public string IdLote { get; set; }

        [XmlElement(ElementName = "MDFe")]
        public Informacoes.MDFe MDFe { get; set; }

        public static MDFeEnviMDFe LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<MDFeEnviMDFe>(xml);
        }

        public static MDFeEnviMDFe LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<MDFeEnviMDFe>(caminhoArquivoXml);
        }
    }
}