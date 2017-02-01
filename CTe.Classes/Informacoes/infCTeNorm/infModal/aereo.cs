using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.InfCTeNormal.Aereo;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class aereo
    {
        private string _nMinu;
        private string _nOCA;
        private string _dPrevAereo;

        public string nMinu { get { return _nMinu; } set { _nMinu = value; } }
        public string nOCA { get { return _nOCA; } set { _nOCA = value; } }
        public string dPrevAereo { get { return _dPrevAereo; } set { _dPrevAereo = value; } }

        public natCarga natCarga { get; set; }

        public tarifa tarifa { get; set; }

        public CTeDLL.Classes.Informacoes.InfCTeNormal.Aereo.peri peri { get; set; }
    }
}