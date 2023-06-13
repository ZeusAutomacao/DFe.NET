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
    public class Issqn
    {
        #region Propriedades

        /// <summary>
        ///     Inscrição Municipal
        /// </summary>
        public string Im { get; set; }

        /// <summary>
        ///     Valor total dos servicço
        /// </summary>
        public decimal ValorTotServ { get; set; }

        /// <summary>
        ///     Base de calculo
        /// </summary>
        public decimal BaseCalculo { get; set; }

        /// <summary>
        ///     Valor ISSQN
        /// </summary>
        public decimal ValorIssqn { get; set; }

        #endregion
    }
}