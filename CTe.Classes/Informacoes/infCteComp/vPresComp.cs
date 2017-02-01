using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeComplementar
{
    public class vPresComp
    {
        public compComp compComp;

        private double _vTPrest;

        public double vTPrest { get { return _vTPrest; } set { _vTPrest = value; } }
    }
}
