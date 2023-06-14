// ===================================================================
//  Empresa: DSBR - Empresa de Desenvolvimento de Sistemas
//  Projeto: DSBR - Automação Comercial
//  Autores:  Valnei Filho, Vagner Marcelo
//  E-mail: dsbrbrasil@yahoo.com.br
//  Data Criação: 10/04/2020
//  Todos os direitos reservados
// ===================================================================


namespace NFe.Danfe.Html.Dominio
{
    public class Produto
    {
        #region Propriedades

        /// <summary>
        ///     Código do produto
        /// </summary>
        public string Codigo { get; }

        /// <summary>
        ///     Descrição do produto
        /// </summary>
        public string Descricao { get; }

        /// <summary>
        ///     Informações adicionais do produto
        /// </summary>
        public string InfAdic { get; }

        /// <summary>
        ///     Unidade do produto
        /// </summary>
        public string Unidade { get; }

        /// <summary>
        ///     Quantidade do produto
        /// </summary>
        public decimal Quantidade { get; }

        /// <summary>
        ///     Valor unitário do produto
        /// </summary>
        public decimal ValorUnitario { get; }

        /// <summary>
        ///     Valor IMCS
        /// </summary>
        public decimal VIcms { get; }

        /// <summary>
        ///     Código NNCM
        /// </summary>
        public string Ncm { get; }

        /// <summary>
        ///     Código CFOP
        /// </summary>
        public string Cfop { get; }

        /// <summary>
        ///     Origem Mercadoria
        /// </summary>
        public string Origem { get; }

        /// <summary>
        ///     Base de calculo
        /// </summary>
        public decimal BaseCalculo { get; }

        /// <summary>
        ///     Percentual ICMS
        /// </summary>
        public decimal PIcms { get; }

        /// <summary>
        ///     IPI
        /// </summary>
        public decimal? ValorIpi { get; }

        /// <summary>
        ///      Percentual IPI
        /// </summary>
        public decimal? PIpi { get; }

        /// <summary>
        /// Valor Total
        /// </summary>
        public decimal ValorTotal { get; set; }

        #endregion

        #region Construtor

        /// <summary>
        ///     NFe
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <param name="descricao">Descrição</param>
        /// <param name="infAdic">Informações adicionais</param>
        /// <param name="unidade">Unidade de medida</param>
        /// <param name="quantidade">Quantidade</param>
        /// <param name="valorUnitario">Valor unitário</param>
        /// <param name="valorIcms">Valor do ICMS</param>
        /// <param name="ncm">Código NCM</param>
        /// <param name="cfop">Código CFOP</param>
        /// <param name="origem">{0- Nacional | 1 Importado}</param>
        /// <param name="baseCalculo">Base de cálculo</param>
        /// <param name="pIcms">Percentual ICMS</param>
        /// <param name="pIpI">Percentual IPI</param>
        /// <param name="vIpi">Valor percentual do IPI</param>
        /// <param name="valorTotal">Valor Total</param>
        public Produto(string codigo, string descricao, string infAdic, string unidade, decimal quantidade, decimal valorUnitario,
                decimal valorIcms, string ncm, string cfop, string origem, decimal baseCalculo, decimal pIcms, decimal? pIpI,
                decimal? vIpi,decimal valorTotal)
        {
            Codigo = codigo;
            Descricao = descricao;
            InfAdic = infAdic;
            Unidade = unidade;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            VIcms = valorIcms;
            Ncm = ncm;
            Cfop = cfop;
            Origem = origem;
            BaseCalculo = baseCalculo;
            PIcms = pIcms;
            ValorIpi = vIpi;
            PIpi = pIpI;
            ValorTotal = valorTotal;
        }

        /// <summary>
        ///     NFCe
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <param name="descricao">Descrição</param>
        /// <param name="unidade">Unidade de medida</param>
        /// <param name="quantidade">Quantidade</param>
        /// <param name="valorUnitario">Valor unitário</param>
        public Produto(string codigo, string descricao, string unidade, decimal quantidade, decimal valorUnitario)
        {
            Codigo = codigo;
            Descricao = descricao;
            Unidade = unidade;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        #endregion
    }
}