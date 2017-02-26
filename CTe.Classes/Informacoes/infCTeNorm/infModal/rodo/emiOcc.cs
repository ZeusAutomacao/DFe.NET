using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class emiOcc
    {
        public string CNPJ { get; set; }

        public string cInt { get; set; }

        public string IE { get; set; }

        [XmlIgnore]
        public Estado UF { get; set; }

        public string ProxyUF
        {
            get
            {
                return UF.GetSiglaUfString();
            }
            set
            {
                UF = UF.SiglaParaEstado(value);
            }
        }

        public string fone { get; set; }
    }
}