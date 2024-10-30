namespace NFe.Classes.Informacoes.Detalhe.Exportacao
{
    public class exportInd
    {
        private decimal _qExport;

        /// <summary>
        ///     I53 - Número do Registro de Exportação
        /// </summary>
        public string nRE { get; set; }

        /// <summary>
        ///     I54 - Chave de Acesso da NF-e recebida para exportação
        /// </summary>
        public string chNFe { get; set; }

        /// <summary>
        ///     I55 - Quantidade do item realmente exportado
        /// </summary>
        public decimal qExport
        {
            get { return _qExport; }
            set { _qExport = value.Arredondar(4); }
        }
    }
}