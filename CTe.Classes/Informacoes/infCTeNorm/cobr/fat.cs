using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class fat
    {
        private string _nFat;
        private double _vOrig;
        private double _vDesc;
        private double _vLiq;

        public string nFat { get { return _nFat; } set { _nFat = value; } }
        public double vOrig { get { return _vOrig; } set { _vOrig = value; } }
        public double vDesc { get { return _vDesc; } set { _vDesc = value; } }
        public double vLiq { get { return _vLiq; } set { _vLiq = value; } }
    }
}
