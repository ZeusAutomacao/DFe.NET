using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Destinatario
{
    public class locEnt
    {
        private string _CNPJ;
        private string _CPF;
        private string _xNome;
        private string _xLgr;
        private string _nro;
        private string _xCpl;
        private string _xBairro;
        private int _cMun;
        private string _xMun;
        private string _UF;

        public string CNPJ { get { return _CNPJ; } set { _CNPJ = value; } }
        public string CPF { get { return _CPF; } set { _CPF = value; } }
        public string xNome { get { return _xNome; } set { _xNome = value; } }
        public string xLgr { get { return _xLgr; } set { _xLgr = value; } }
        public string nro { get { return _nro; } set { _nro = value; } }
        public string xCpl { get { return _xCpl; } set { _xCpl = value; } }
        public string xBairro { get { return _xBairro; } set { _xBairro = value; } }
        public int cMun { get { return _cMun; } set { _cMun = value; } }
        public string xMun { get { return _xMun; } set { _xMun = value; } }
        public string UF { get { return _UF; } set { _UF = value; } }
    }
}
