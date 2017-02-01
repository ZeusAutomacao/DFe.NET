using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infUnidCarga
    {
        public int tpUnidCarga { get; set; }

        public string idUnidCarga { get; set; }

        [XmlElement("lacUnidCarga")]
        public lacUnidCarga lacUnidCarga { get; set; }

        public decimal? qtdRat { get; set; }
    }
}
