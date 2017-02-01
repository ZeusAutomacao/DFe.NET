using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Impostos.Tipos;

namespace CTeDLL.Classes.Informacoes.Impostos
{
    public class ICMS60 : ICMSBasico
    {
        private string _CST;
        private double _vBCSTRet;
        private double _vICMSSTRet;
        private double _pICMSSTRet;
        private double _vCred;

        public string CST { get { return _CST; } set { _CST = value; } }
        public double vBCSTRet { get { return _vBCSTRet; } set { _vBCSTRet = value; } }
        public double vICMSSTRet { get { return _vICMSSTRet; } set { _vICMSSTRet = value; } }
        public double pICMSSTRet { get { return _pICMSSTRet; } set { _pICMSSTRet = value; } }
        public double vCred { get { return _vCred; } set { _vCred = value; } }
    }
}
