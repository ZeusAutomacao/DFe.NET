using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class tarifa
    {
        private decimal _vTar;
        public CL CL { get; set; }

        public string cTar { get; set; }

        public decimal vTar
        {
            get { return _vTar.Arredondar(2); }
            set { _vTar = value.Arredondar(2); }
        }
    }
}