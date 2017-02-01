using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infUnidTransp
    {
        public int tpUnidTransp { get; set; }

        public string idUnidTransp { get; set; }

        [XmlElement("lacUnidTransp")]
        public lacUnidTransp lacUnidTransp { get; set; }

        [XmlElement("infUnidCarga")]
        public infUnidCarga infUnidCarga { get; set; }

        public decimal? qtdRat { get; set; }
    }
}
