using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Remetente
{
    public class infOutros
    {
        private int _tpDoc;
        private string _descOutros;
        private string _nDoc;
        private string _dEmi;
        private double _vDocFisc;

        public int tpDoc { get { return _tpDoc; } set { _tpDoc = value; } }
        public string descOutros { get { return _descOutros; } set { _descOutros = value; } }
        public string nDoc { get { return _nDoc; } set { _nDoc = value; } }
        public string dEmi { get { return _dEmi; } set { _dEmi = value; } }
        public double vDocFisc { get { return _vDocFisc; } set { _vDocFisc = value; } }
    }
}
