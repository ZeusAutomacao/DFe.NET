using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class rodo
    {
        private string _RNTRC;

        public string RNTRC { get { return _RNTRC; } set { _RNTRC = value; } }

        [XmlElement("occ")]
        public List<occ> occ;
    }
}