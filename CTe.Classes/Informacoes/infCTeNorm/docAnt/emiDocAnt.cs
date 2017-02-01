using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class emiDocAnt
    {
        public idDocAnt idDocAnt;

        private string _CNPJ;
        private string _CPF;
        private string _IE;
        private string _UF;
        private string _xNome;

        public string CNPJ { get { return _CNPJ; } set { _CNPJ = value; } }
        public string CPF { get { return _CPF; } set { _CPF = value; } }
        public string IE { get { return _IE; } set { _IE = value; } }
        public string UF { get { return _UF; } set { _UF = value; } }
        public string xNome { get { return _xNome; } set { _xNome = value; } }
    }
}
