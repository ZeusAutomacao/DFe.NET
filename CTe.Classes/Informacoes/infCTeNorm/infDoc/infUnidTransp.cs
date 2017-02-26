using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infUnidTransp
    {
        public tpUnidTransp tpUnidTransp { get; set; }

        public string idUnidTransp { get; set; }

        [XmlElement("lacUnidTransp")]
        public List<lacUnidTransp> lacUnidTransp { get; set; }

        [XmlElement("infUnidCarga")]
        public List<infUnidCarga> infUnidCarga { get; set; }

        public decimal? qtdRat { get; set; }

        public bool qtdRatSpecified => qtdRat.HasValue;
    }
}
