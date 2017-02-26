using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class fat
    {
        public string nFat { get; set; }

        public decimal? vOrig { get; set; }

        public decimal? vDesc { get; set; }

        public decimal? vLiq { get; set; }


        public bool vOrigSpecified => vOrig.HasValue;
        public bool vDescSpecified => vDesc.HasValue;
        public bool vLiqSpecified => vLiq.HasValue;
    }
}
