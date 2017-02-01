using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Valores
{
    public class Comp
    {
        private string _xNome;
        private double _vComp;

        public string xNome { get { return _xNome; } set { _xNome = value; } }
        public double vComp { get { return _vComp; } set { _vComp = value; } }
    }
}
