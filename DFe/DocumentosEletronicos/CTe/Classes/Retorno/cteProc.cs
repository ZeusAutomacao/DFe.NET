using System;
using System.Xml.Serialization;
using DFe.DocumentosEletronicos.Flags;

namespace DFe.DocumentosEletronicos.CTe.Classes.Retorno
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "cteProc")]
    public class cteProc
    {
        [XmlAttribute]
        public VersaoServico versao { get; set; }

        public Informacoes.CTe CTe { get; set; }

        public protCTe protCTe { get; set; }
    }
}