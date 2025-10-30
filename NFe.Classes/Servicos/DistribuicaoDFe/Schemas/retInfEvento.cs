using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class retInfEvento
    {
        public byte tpAmb { get; set; }

        public string verAplic { get; set; }

        public byte cOrgao { get; set; }

        public byte cStat { get; set; }

        public string xMotivo { get; set; }

        public string chNFe { get; set; }

        public uint tpEvento { get; set; }

        public string xEvento { get; set; }

        public byte nSeqEvento { get; set; }

        public string CNPJDest { get; set; }

        public string emailDest { get; set; }

        public DateTime dhRegEvento { get; set; }

        public string nProt { get; set; }
    }
}