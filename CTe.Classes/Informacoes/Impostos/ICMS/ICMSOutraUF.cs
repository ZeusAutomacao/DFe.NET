using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMSOutraUF : ICMSBasico
    {
        private string _CST;
        private double _pRedBCOutraUF;
        private double _vBCOutraUF;
        private double _pICMSOutraUF;
        private double _vICMSOutraUF;

        public string CST { get { return _CST; } set { _CST = value; } }
        public double pRedBCOutraUF { get { return _pRedBCOutraUF; } set { _pRedBCOutraUF = value; } }
        public double vBCOutraUF { get { return _vBCOutraUF; } set { _vBCOutraUF = value; } }
        public double pICMSOutraUF { get { return _pICMSOutraUF; } set { _pICMSOutraUF = value; } }
        public double vICMSOutraUF { get { return _vICMSOutraUF; } set { _vICMSOutraUF = value; } }
    }
}
