using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeComplementar
{
    public class compComp
    {
        private string _xNome;
        private double _vComp;

        public string xNome { get { return _xNome; } set { _xNome = value; } }
        public double vComp { get { return _vComp; } set { _vComp = value; } }
    }
}
