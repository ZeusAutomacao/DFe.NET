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
    public class ProdutoNFCe
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
        ///     Valor total do produto
        /// </summary>
        public decimal ValorTotal { get; }

        #endregion

        #region Construtor

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public ProdutoNFCe(string codigo, string descricao, string unidade, decimal quantidade, decimal valorUnitario, decimal valorTotal)
        {
            Codigo = codigo;
            Descricao = descricao;
            Unidade = unidade;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ValorTotal = valorTotal;
        }

        #endregion
    }
}