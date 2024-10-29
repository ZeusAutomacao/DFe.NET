using CTe.Classes.Informacoes.Tipos;

namespace CTe.Classes.Informacoes.Identificacao
{
    public class toma4
    {
        public toma4()
        {
            toma = toma.Outros;
        }

        public toma toma { get; set; }

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