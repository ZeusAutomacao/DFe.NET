using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal.cobrancas;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal
{
    [Serializable]
    public class cobr
    {
        [XmlElement("fat", Order = 1)]
        public fat fat { get; set; }

        [XmlElement("dup", Order = 2)]
        public List<dup> dup;
    }
}