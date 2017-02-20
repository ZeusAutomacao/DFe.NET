namespace CTeDLL.Classes.Informacoes.Emitente
{
    public class emit
    {
        public string CNPJ { get; set; }

        public string IE { get; set; }

        /// <summary>
        /// Versão 3.00 - Não é obrigatório
        /// </summary>
        public string IEST { get; set; }

        public string xNome { get; set; }

        public string xFant { get; set; }

        public enderEmit enderEmit { get; set; }
    }
}
