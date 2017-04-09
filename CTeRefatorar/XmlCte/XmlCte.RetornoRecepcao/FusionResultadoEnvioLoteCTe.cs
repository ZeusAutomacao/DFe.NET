using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.RetornoRecepcao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte", ElementName = "retConsReciCTe")]
    public class FusionResultadoEnvioLoteCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "verAplic")]
        public string VersaoAplicativo { get; set; }

        [XmlElement(ElementName = "nRec")]
        public string NumeroRecibo { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short CodigoStatusResposta { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string Motivo { get; set; }

        [XmlElement(ElementName = "cUF")]
        public byte CodigoUfAtendeuSolicitacao { get; set; }

        [XmlElement(ElementName = "protCTe")]
        public List<FusionCteProcessadoCTe> CteProcessado { get; set; }
    }
}