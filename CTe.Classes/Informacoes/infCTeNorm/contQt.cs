using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class contQt
    {
        public lacContQt lacContQt;

        private string _nCont;
        private string _dPrev;

        public string nCont { get { return _nCont; } set { _nCont = value; } }
        public string dPrev { get { return _dPrev; } set { _dPrev = value; } }
    }
}
