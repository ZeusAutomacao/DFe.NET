using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Complemento;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.Complemento
{
    public class complOs
    {
        public string xCaracAd { get; set; }

        public string xCaracSer { get; set; }

        public string xEmi { get; set; }

        public string xObs { get; set; }

        [XmlElement(ElementName = "ObsCont")]
        public List<ObsCont> ObsCont { get; set; }

        [XmlElement(ElementName = "ObsFisco")]
        public List<ObsFisco> ObsFisco { get; set; }
    }
}