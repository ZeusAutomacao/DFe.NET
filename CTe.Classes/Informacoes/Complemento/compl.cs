using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Complemento
{
    public class compl
    {
        public string xCaracAd { get; set; }

        public string xCaracSer { get; set; }

        public string xEmi { get; set; }

        public fluxo fluxo { get; set; }

        public Entrega Entrega { get; set; }

        public string origCalc { get; set; }

        public string destCalc { get; set; }

        public string xObs { get; set; }

        [XmlElement(ElementName = "ObsCont")]
        public List<ObsCont> ObsCont { get; set; }

        [XmlElement(ElementName = "ObsFisco")]
        public List<ObsFisco> ObsFisco { get; set; }
    }
}