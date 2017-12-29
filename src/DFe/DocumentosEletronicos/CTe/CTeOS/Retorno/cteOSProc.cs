using System;
using System.Xml.Serialization;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno;
using DFe.DocumentosEletronicos.Flags;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Retorno
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "cteOSProc")]
    public class cteOSProc
    {
        [XmlAttribute]
        public VersaoServico versao { get; set; }

        public CTeOS CTeOS { get; set; }

        public protCTe protCTe { get; set; }
    }
}