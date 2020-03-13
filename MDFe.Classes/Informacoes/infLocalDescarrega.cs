using System;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infLocalDescarrega
    {
        public string CEP { get; set; }

        public decimal latitude { get; set; }

        public decimal Logintude { get; set; }
    }
}