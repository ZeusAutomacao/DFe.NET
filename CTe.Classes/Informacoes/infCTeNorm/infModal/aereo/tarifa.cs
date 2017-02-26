using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class tarifa
    {
        public CL CL { get; set; }

        public string cTar { get; set; }

        public decimal vTar { get; set; }
    }
}