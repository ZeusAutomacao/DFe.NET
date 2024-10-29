namespace NFe.Classes.Informacoes.Identificacao
{
    public class refECF
    {
        /// <summary>
        ///     BA21 - Modelo do Documento Fiscal
        /// </summary>
        public string mod { get; set; }

        /// <summary>
        ///     BA22 - Número de ordem sequencial do ECF
        /// </summary>
        public int nECF { get; set; }

        /// <summary>
        ///     BA23 - Número do Contador de Ordem de Operação - COO
        /// </summary>
        public int nCOO { get; set; }
    }
}