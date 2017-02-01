using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class tarifa
    {
        private string _CL;
        private string _cTar;
        private string _vTar;

        public string CL { get { return _CL; } set { _CL = value; } }
        public string cTar { get { return _cTar; } set { _cTar = value; } }
        public string vTar { get { return _vTar; } set { _vTar = value; } }

    }
}