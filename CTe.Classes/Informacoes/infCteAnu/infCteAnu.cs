using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeAnulacao
{
    public class infCteAnu
    {
        private string _chCte;
        private string _dEmi;

        public string chCte { get { return _chCte; } set { _chCte = value; } }
        public string dEmi { get { return _dEmi; } set { _dEmi = value; } }
    }
}
