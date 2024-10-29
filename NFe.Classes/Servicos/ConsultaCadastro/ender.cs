namespace NFe.Classes.Servicos.ConsultaCadastro
{
    public class ender
    {
        /// <summary>
        ///     GR23 - Nome do Logradouro
        /// </summary>
        public string xLgr { get; set; }

        /// <summary>
        ///     GR24 - Número
        /// </summary>
        public string nro { get; set; }

        /// <summary>
        ///     GR25 - Complemento
        /// </summary>
        public string xCpl { get; set; }

        /// <summary>
        ///     GR26 - Nome do Bairro
        /// </summary>
        public string xBairro { get; set; }

        /// <summary>
        ///     GR27 - Código do Município do Contribuinte, conforme Tabela do IBGE
        /// </summary>
        public string cMun { get; set; }

        /// <summary>
        ///     GR28 - Nome do município
        /// </summary>
        public string xMun { get; set; }

        /// <summary>
        ///     GR29 - Código do CEP
        /// </summary>
        public string CEP { get; set; }
    }
}