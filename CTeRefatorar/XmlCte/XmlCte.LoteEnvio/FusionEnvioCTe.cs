using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.LoteEnvio
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "enviCTe")]
    public class FusionEnvioCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; } = "2.00";

        [XmlElement(ElementName = "idLote")]
        public string IdLote { get; set; }

        [XmlElement(ElementName = "CTe")]
        public List<FusionCTe> FusionCTes { get; set; }

        public FusionEnvioCTe()
        {
            FusionCTes = new List<FusionCTe>();
        }
    }
}