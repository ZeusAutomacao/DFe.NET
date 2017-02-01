using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class dup
    {
        private string _nDup;
        private string _dVenc;
        private double _vDup;

        public string nDup { get { return _nDup; } set { _nDup = value; } }
        public string dVenc { get { return _dVenc; } set { _dVenc = value; } }
        public double vDup { get { return _vDup; } set { _vDup = value; } }
    }
}
