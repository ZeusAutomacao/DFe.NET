using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class idDocAntPap
    {
        private int _tpDoc;
        private string _serie;
        private string _subser;
        private string _nDoc;
        private string _dEmi;

        public int tpDoc { get { return _tpDoc; } set { _tpDoc = value; } }
        public string serie { get { return _serie; } set { _serie = value; } }
        public string subser { get { return _subser; } set { _subser = value; } }
        public string nDoc { get { return _nDoc; } set { _nDoc = value; } }
        public string dEmi { get { return _dEmi; } set { _dEmi = value; } }
    }
}
