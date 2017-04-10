using System;
using System.Xml.Serialization;
using CTe.Classes.Protocolo;
using CTe.Classes.Servicos.Tipos;

namespace CTe.Classes
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "cteProc")]
    public class cteProc
    {
        [XmlAttribute]
        public versao versao { get; set; }

        public CTe CTe { get; set; }

        public protCTe protCTe { get; set; }
    }
}