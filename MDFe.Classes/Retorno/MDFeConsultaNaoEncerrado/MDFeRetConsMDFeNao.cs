using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeConsultaNaoEncerrado
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "retConsMDFeNaoEnc")]
    public class MDFeRetConsMDFeNao
    {
        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VerAplic { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CStat { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string XMotivo { get; set; }

        [XmlIgnore]
        public EstadoUF CUF { get; set; }

        [XmlElement(ElementName = "cUF")]
        public string CUFProxy
        {
            get
            {
                return CUF.GetCodigoIbgeEmString();
            }
            set { CUF = CUF.CodigoIbgeParaEstado(value); }
        }

        [XmlElement(ElementName = "infMDFe")]
        public List<MDFeNaoEncerradaInfMDFe> InfMDFe { get; set; }
    }

    [Serializable]
    public class MDFeNaoEncerradaInfMDFe
    {
        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }
        [XmlElement(ElementName = "nProt")]
        public string NProt { get; set; }
    }
}