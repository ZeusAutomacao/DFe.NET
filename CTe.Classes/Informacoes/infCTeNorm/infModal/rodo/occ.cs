using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class occ
    {
        private string _serie;
        private string _nOcc;
        private string _dEmi;

        public string serie { get { return _serie; } set { _serie = value; } }
        public string nOcc { get { return _nOcc; } set { _nOcc = value; } }
        public string dEmi { get { return _dEmi; } set { _dEmi = value; } }
        public emiOcc emiOcc { get; set; }
    }
}
