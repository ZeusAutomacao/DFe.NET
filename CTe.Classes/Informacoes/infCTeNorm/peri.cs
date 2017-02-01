using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class peri  //versão 2.00
    {
        private string _nONU;
        private string _xNomeAE;
        private string _xClaRisco;
        private string _grEmb;
        private string _qTotProd;
        private string _qVolTipo;
        private string _pontoFulgor;

        public string nONU { get { return _nONU; } set { _nONU = value; } }
        public string xNomeAE { get { return _xNomeAE; } set { _xNomeAE = value; } }
        public string xClaRisco { get { return _xClaRisco; } set { _xClaRisco = value; } }
        public string grEmb { get { return _grEmb; } set { _grEmb = value; } }
        public string qTotProd { get { return _qTotProd; } set { _qTotProd = value; } }
        public string qVolTipo { get { return _qVolTipo; } set { _qVolTipo = value; } }
        public string pontoFulgor { get { return _pontoFulgor; } set { _pontoFulgor = value; } }
    }
}
