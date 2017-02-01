using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMS00 : ICMSBasico
    {
        private string _CST;
        private double _vBC;
        private double _plCMS;
        private double _vlCMS;

        public string CST { get { return _CST; } set { _CST = value; } }
        public double vBC { get { return _vBC; } set { _vBC = value; } }
        public double plCMS { get { return _plCMS; } set { _plCMS = value; } }
        public double vlCMS { get { return _vlCMS; } set { _vlCMS = value; } }
    }
}
