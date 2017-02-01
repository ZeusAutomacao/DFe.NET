using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infQ
    {
        private string _cUnid;
        private string _tpMed;
        private double _qCarga;

        public string cUnid { get { return _cUnid; } set { _cUnid = value; } }
        public string tpMed { get { return _tpMed; } set { _tpMed = value; } }
        public double qCarga { get { return _qCarga; } set { _qCarga = value; } }
    }
}
