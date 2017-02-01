using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal.Aereo
{
    public class peri
    {
        private string _nONU;
        private string _qTotEmb;

        public string nONU { get { return _nONU; } set { _nONU = value; } }
        public string qTotEmb { get { return _qTotEmb; } set { _qTotEmb = value; } }

    }
}