using System;
using System.Xml.Serialization;
using DFe.DocumentosEletronicos.CTe.Classes.Flags;

namespace DFe.DocumentosEletronicos.CTe.Classes.Retorno
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "cteProc")]
    public class cteProc
    {
        [XmlAttribute]
        public versao versao { get; set; }

        public Informacoes.CTe CTe { get; set; }

        public protCTe protCTe { get; set; }
    }
}