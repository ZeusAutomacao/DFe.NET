using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class natCarga
    {
        private string _xDime;
        private string _cInfManu;

        public string xDime { get { return _xDime; } set { _xDime = value; } }
        public string cInfManu { get { return _cInfManu; } set { _cInfManu = value; } }

    }
}