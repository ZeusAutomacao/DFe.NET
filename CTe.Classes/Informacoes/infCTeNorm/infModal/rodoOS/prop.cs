using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class prop
    {
        private string _CPF;
        private string _CNPJ;
        private string _TAF;
        private string _NroRegEstadual;
        private string _xNome;
        private string _IE;
        private string _UF;
        private string _tpProp;


        public string CPF { get { return _CPF; } set { _CPF = value; } }
        public string CNPJ { get { return _CNPJ; } set { _CNPJ = value; } }
        public string TAF { get { return _TAF; } set { _TAF = value; } }
        public string NroRegEstadual { get { return _NroRegEstadual; } set { _NroRegEstadual = value; } }
        public string xNome { get { return _xNome; } set { _xNome = value; } }
        public string IE { get { return _IE; } set { _IE = value; } }
        public string UF { get { return _UF; } set { _UF = value; } }
        public string tpProp { get { return _tpProp; } set { _tpProp = value; } }

    }
}