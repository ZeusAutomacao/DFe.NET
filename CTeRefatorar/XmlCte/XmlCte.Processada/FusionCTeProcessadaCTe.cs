using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.Processada
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "cteProc")]
    public class FusionCTeProcessadaCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; } = "2.00";

        [XmlElement(ElementName = "CTe")]
        public FusionCTe CTe { get; set; }

        [XmlElement(ElementName = "protCTe")]
        public FusionCteProcessadoCTe ProcessadoCTe { get; set; }

        public FusionCTeProcessadaCTe()
        {
            ProcessadoCTe = new FusionCteProcessadoCTe();
        }
    }

    [Serializable]
    public class FusionCteProcessadoCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; } = "2.00";

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VersaoAplicativo { get; set; }

        [XmlElement(ElementName = "chCTe")]
        public string Chave { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string RecebidaEm { get; set; }

        [XmlElement(ElementName = "nProt")]
        public string Protocolo { get; set; }

        [XmlElement(ElementName = "digVal")]
        public string DigestValue { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CodigoStatus { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string Motivo { get; set; }
    }
}