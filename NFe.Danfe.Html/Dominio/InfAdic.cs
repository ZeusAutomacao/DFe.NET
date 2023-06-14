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
    public class InfAdic
    {
        #region Propriedades

        /// <summary>
        ///     Informações complementares
        /// </summary>
        public string InfComplementares { get; }

        /// <summary>
        ///     Informações destinadas ao fisco
        /// </summary>
        public string InfFisco { get; }

        
        public InfAdic(string infComplementares, string infFisco)
        {
            InfComplementares = infComplementares;
            InfFisco = infFisco;    
        }

        #endregion
    }
}