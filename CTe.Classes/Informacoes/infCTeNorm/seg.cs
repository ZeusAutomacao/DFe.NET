using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class seg
    {
        public respSeg respSeg { get; set; }

        public string xSeg { get; set; }

        public string nApol { get; set; }

        public string nAver { get; set; }

        public decimal? vCarga { get; set; }
        public bool vCargaSpecified => vCarga.HasValue;
    }
}
