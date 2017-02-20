namespace CTeDLL.Classes.Informacoes.Remetente
{
    public class rem
    {
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        public string IE { get; set; }

        public string xNome { get; set; }

        public string xFant { get; set; }

        public string fone { get; set; }

        public enderReme enderReme { get; set; }

        public string email { get; set; }

        /// <summary>
        /// Versao 2.00
        /// </summary>
        public locColeta locColeta { get; set; }
    }
}
