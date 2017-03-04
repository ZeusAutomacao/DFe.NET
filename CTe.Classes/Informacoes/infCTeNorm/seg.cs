using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class seg
    {
        private decimal? _vCarga;
        public respSeg respSeg { get; set; }

        public string xSeg { get; set; }

        public string nApol { get; set; }

        public string nAver { get; set; }

        public decimal? vCarga
        {
            get { return _vCarga.Arredondar(2); }
            set { _vCarga = value.Arredondar(2); }
        }

        public bool vCargaSpecified => vCarga.HasValue;
    }
}
