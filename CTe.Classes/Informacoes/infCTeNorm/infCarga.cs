using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infCarga
    {
        [XmlElement("infQ")]
        public List<infQ> infQ;

        private double _vCarga;
        private string _proPred;
        private string _xOutCat;

        public double vCarga { get { return _vCarga; } set { _vCarga = value; } }
        public string proPred { get { return _proPred; } set { _proPred = value; } }
        public string xOutCat { get { return _xOutCat; } set { _xOutCat = value; } }
    }
}
