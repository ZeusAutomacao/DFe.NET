using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class rodoOS : ContainerModal
    {
        public string TAF { get; set; }

        public string NroRegEstadual { get; set; }

        public veic veic { get; set; }
    }
}