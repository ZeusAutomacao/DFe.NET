using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;

namespace CTeDLL.Classes.Informacoes.Identificacao
{
    public class toma4
    {
        public toma toma { get; set; } = toma.Outros;

        public string CNPJ { get; set; }

        public string CPF { get; set; }

        public string IE { get; set; }

        public string xNome { get; set; }

        public string xFant { get; set; }

        public string fone { get; set; }

        public enderToma enderToma { get; set; }

        public string email { get; set; }

    }
}
