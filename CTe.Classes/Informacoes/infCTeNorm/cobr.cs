using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class cobr
    {
        public fat fat;
        [XmlElement("dup")]
        public List<dup> dup;
    }
}
