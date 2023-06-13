// ===================================================================
//  Empresa: DSBR - Empresa de Desenvolvimento de Sistemas
//  Projeto: DSBR - Automação Comercial
//  Autores:  Valnei Filho, Vagner Marcelo
//  E-mail: dsbrbrasil@yahoo.com.br
//  Data Criação: 10/04/2020
//  Todos os direitos reservados
// ===================================================================


#region

using System.Collections.Generic;

#endregion

namespace NFe.Danfe.Html.Dominio
{
    public class Pagamento
    {
        #region Propriedades

        /// <summary>
        ///     Valor total em compras
        /// </summary>
        public decimal ValorTotal { get; }

        /// <summary>
        ///     Valor total em descontos
        /// </summary>
        public decimal ValorTotDesconto { get; }

        /// <summary>
        ///     Valor total devido
        /// </summary>
        public decimal ValorTotDevido { get; }

        /// <summary>
        ///     Troco devido
        /// </summary>
        public decimal? Troco { get; }

        /// <summary>
        ///     Formas de pagamento
        /// </summary>
        public ICollection<FormPag> FormasPagamentos { get; }

        #endregion

        #region Construtor

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public Pagamento(decimal valorTotal, decimal valorTotDesconto, decimal valorTotDevido, decimal? troco,
                ICollection<FormPag> formasPagamentos)
        {
            ValorTotal = valorTotal;
            ValorTotDesconto = valorTotDesconto;
            ValorTotDevido = valorTotDevido;
            Troco = troco;
            FormasPagamentos = formasPagamentos;
        }

        #endregion
    }
}