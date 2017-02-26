using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infUnidCarga
    {
        public tpUnidCarga tpUnidCarga { get; set; }

        public string idUnidCarga { get; set; }

        [XmlElement("lacUnidCarga")]
        public List<lacUnidCarga> lacUnidCarga { get; set; }

        public decimal? qtdRat { get; set; }

        public bool qtdRatSpecified => qtdRat.HasValue;
    }
}
