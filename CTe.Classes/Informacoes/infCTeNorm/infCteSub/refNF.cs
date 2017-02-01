using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class refNF
    {
        private string _CNPJ;
        private string _mod;
        private int _serie;
        private int _subserie;
        private int _nro;
        private double _valor;
        private string _dEmi;

        public string CNPJ { get { return _CNPJ; } set { _CNPJ = value; } }
        public string mod { get { return _mod; } set { _mod = value; } }
        public int serie { get { return _serie; } set { _serie = value; } }
        public int subserie { get { return _subserie; } set { _subserie = value; } }
        public int nro { get { return _nro; } set { _nro = value; } }
        public double valor { get { return _valor; } set { _valor = value; } }
        public string dEmi { get { return _dEmi; } set { _dEmi = value; } }
    }
}
