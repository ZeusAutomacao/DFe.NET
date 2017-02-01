using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class rodoOS
    {
        private string _TAF;
        private string _NroRegEstadual;

        public string TAF { get { return _TAF; } set { _TAF = value; } }
        public string NroRegEstadual { get { return _NroRegEstadual; } set { _NroRegEstadual = value; } }

        public veic veic { get; set; }
    }
}