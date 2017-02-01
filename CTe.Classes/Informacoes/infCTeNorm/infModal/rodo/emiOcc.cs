using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class emiOcc
    {
        private string _CNPJ;
        private string _cInt;
        private string _IE;
        private string _UF;
        private string _fone;

        public string CNPJ { get { return _CNPJ; } set { _CNPJ = value; } }
        public string cInt { get { return _cInt; } set { _cInt = value; } }
        public string IE { get { return _IE; } set { _IE = value; } }
        public string UF { get { return _UF; } set { _UF = value; } }
        public string fone { get { return _fone; } set { _fone = value; } }

    }
}