using System;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.ConsultaGtin
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class retConsGTIN : IRetornoServico
    {
        [XmlAttribute]
        public string versao { get; set; }
        public string verAplic { get; set; }
        public int cStat { get; set; }
        public string xMotivo { get; set; }
        public DateTime dhResp { get; set; }
        public string GTIN { get; set; }
        public string tpGTIN { get; set; }
        public string xProd { get; set; }
        public string NCM { get; set; }
        public string CEST { get; set; }
    }
}

