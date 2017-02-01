using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Identificacao
{
    public class enderToma
    {
        private string _xLgr;
        private string _nro;
        private string _xCpl;
        private string _xBairro;
        private string _cMun;
        private string _xMun;
        private string _CEP;
        private string _UF;
        private int _cPais;
        private string _xPais;
        private string _email;
        private string _dhCont;
        private string _xJust;

        public string xLgr { get { return _xLgr; } set { _xLgr = value; } }
        public string nro { get { return _nro; } set { _nro = value; } }
        public string xCpl { get { return _xCpl; } set { _xCpl = value; } }
        public string xBairro { get { return _xBairro; } set { _xBairro = value; } }
        public string cMun { get { return _cMun; } set { _cMun = value; } }
        public string xMun { get { return _xMun; } set { _xMun = value; } }
        public string CEP { get { return _CEP; } set { _CEP = value; } }
        public string UF { get { return _UF; } set { _UF = value; } }
        public int cPais { get { return _cPais; } set { _cPais = value; } }
        public string xPais { get { return _xPais; } set { _xPais = value; } }
        public string email { get { return _email; } set { _email = value; } }
        public string dhCont { get { return _dhCont; } set { _dhCont = value; } }
        public string xJust { get { return _xJust; } set { _xJust = value; } }
    }
}
