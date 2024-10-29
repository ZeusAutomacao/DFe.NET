namespace NFe.Classes.Informacoes
{
    public class avulsa
    {
        private decimal _vDar;

        /// <summary>
        ///     D02 - CNPJ do órgão emitente
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        ///     D03 - Órgão emitente
        /// </summary>
        public string xOrgao { get; set; }

        /// <summary>
        ///     D04 - Matrícula do agente do Fisco
        /// </summary>
        public string matr { get; set; }

        /// <summary>
        ///     D05 - Nome do agente do Fisco
        /// </summary>
        public string xAgente { get; set; }

        /// <summary>
        ///     D06 - Telefone
        /// </summary>
        public string fone { get; set; }

        /// <summary>
        ///     D07 - Sigla da UF
        /// </summary>
        public string UF { get; set; }

        /// <summary>
        ///     D08 - Número do Documento de Arrecadação de Receita
        /// </summary>
        public string nDAR { get; set; }

        /// <summary>
        ///     D09 - Data de emissão do Documento de Arrecadação
        /// </summary>
        public string dEmi { get; set; }

        /// <summary>
        ///     D10 - Valor Total constante no Documento de arrecadação de Receita
        /// </summary>
        public decimal vDAR
        {
            get { return _vDar; }
            set { _vDar = value.Arredondar(2); }
        }

        /// <summary>
        ///     D11 - Repartição Fiscal emitente
        /// </summary>
        public string repEmi { get; set; }

        /// <summary>
        ///     D12 - Data de pagamento do Documento de Arrecadação
        /// </summary>
        public string dPag { get; set; }
    }
}