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
    /// <summary>
    ///     Formas de pagamento
    /// </summary>
    public class FormPag
    {
        #region Propriedades

        /// <summary>
        ///     Forma de pagamento
        ///     <para>Dinheiro, Cartão, Débito, etc...</para>
        /// </summary>
        public string Forma { get; }

        /// <summary>
        ///     Valor
        /// </summary>
        public decimal Valor { get; }

        #endregion

        #region Construtor

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public FormPag(string forma, decimal valor)
        {
            Forma = forma;
            Valor = valor;
        }

        #endregion
    }
}