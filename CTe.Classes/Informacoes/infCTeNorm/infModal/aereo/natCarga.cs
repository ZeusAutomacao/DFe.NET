using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class natCarga
    {
        public string xDime { get; set; }

        public List<string> cInfManu { get; set; }
        public List<string> cIMP { get; set; }
    }
}