using System;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infPag
    {
        public string xNome { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string idEstrangeiro { get; set; }
    }
}