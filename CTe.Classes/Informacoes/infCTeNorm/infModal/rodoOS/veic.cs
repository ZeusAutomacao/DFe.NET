using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class veic
    {
        private string _placa;
        private string _RENAVAM;
        private string _UF;

        public string placa { get { return _placa; } set { _placa = value; } }
        public string RENAVAM { get { return _RENAVAM; } set { _RENAVAM = value; } }

        public prop prop { get; set; }

        public string UF { get { return _UF; } set { _UF = value; } }
    }
}