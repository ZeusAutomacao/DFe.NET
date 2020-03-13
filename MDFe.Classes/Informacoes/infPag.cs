using System;
using System.Collections.Generic;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infPag
    {
        public string xNome { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string idEstrangeiro { get; set; }

        public List<Comp> Comp { get; set; }

        public decimal vContrato { get; set; }
        public indPag indPag { get; set; }

        public List<infPrazo> infPrazo { get; set; }
    }
}