namespace CTe.Classes.Informacoes.Emitente
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


        /// <summary>
        /// Versão 4.00 é Obrigatório
        /// </summary>
        public CRT? CRT { get; set; }

        /// <summary>
        /// Se null, não aparece no xml
        /// </summary>
        public bool CRTSpecified { get { return CRT.HasValue; } }
    }
}