using DFe.Classes.Entidades;

namespace NFe.Classes.Informacoes.Identificacao
{
    public class refNFP
    {
        /// <summary>
        ///     BA11 - Código da UF do emitente
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     BA12 - Ano e Mês de emissão da NF-e
        /// </summary>
        public string AAMM { get; set; }

        /// <summary>
        ///     BA13 - CNPJ do emitente
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        ///     BA14 - CPF do emitente
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        ///     BA15 - IE do emitente
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        ///     BA16 - Modelo do Documento Fiscal
        /// </summary>
        public string mod { get; set; }

        /// <summary>
        ///     BA17 - Série do Documento Fiscal
        /// </summary>
        public int serie { get; set; }

        /// <summary>
        ///     BA18 - Número do Documento Fiscal
        /// </summary>
        public int nNF { get; set; }
    }
}