using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Destinatario
{
    public class enderDest
    {
        private string _xLgr;
        private string _nro;
        private string _xCpl;
        private string _xBairro;
        private int _cMun;
        private string _xMun;
        private string _UF;
        private string _CEP;
        private int _cPais;
        private string _xPais;

        public string xLgr { get { return _xLgr; } set { _xLgr = value; } }
        public string nro { get { return _nro; } set { _nro = value; } }
        public string xCpl { get { return _xCpl; } set { _xCpl = value; } }
        public string xBairro { get { return _xBairro; } set { _xBairro = value; } }
        public int cMun { get { return _cMun; } set { _cMun = value; } }
        public string xMun { get { return _xMun; } set { _xMun = value; } }
        public string UF { get { return _UF; } set { _UF = value; } }
        public string CEP { get { return _CEP; } set { _CEP = value; } }
        public int cPais { get { return _cPais; } set { _cPais = value; } }
        public string xPais { get { return _xPais; } set { _xPais = value; } }        
    }
}
