using System;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Classes.Informacoes.ConsultaNaoEncerrados
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "consMDFeNaoEnc")]
    public class MDFeCosMDFeNaoEnc
    {
        public MDFeCosMDFeNaoEnc()
        {
            XServ = "CONSULTAR NÃO ENCERRADOS";
        }

        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string XServ { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }
    }
}