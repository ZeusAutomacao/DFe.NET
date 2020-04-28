using System;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infBanc
    {
        public string codBanco { get; set; }
        public string codAgencia { get; set; }
        public string CNPJIPEF { get; set; }
    }
}