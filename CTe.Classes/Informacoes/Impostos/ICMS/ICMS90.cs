using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMS90 : ICMSBasico
    {
        private string _CST;
        private double _pRedBC;
        private double _vBC;
        private double _pICMS;
        private double _vICMS;
        private double _vCred;

        public string CST { get { return _CST; } set { _CST = value; } }
        public double pRedBC { get { return _pRedBC; } set { _pRedBC = value; } }
        public double vBC { get { return _vBC; } set { _vBC = value; } }
        public double plCMS { get { return _pICMS; } set { _pICMS = value; } }
        public double vlCMS { get { return _vICMS; } set { _vICMS = value; } }
        public double vCred { get { return _vCred; } set { _vCred = value; } }
    }
}
