using System.Threading.Tasks;
using NFe.Danfe.Html.Dominio;

namespace NFe.Danfe.Html.Interfaces
{
    public interface IDanfeHtml2
    {
        #region Propriedades

        /// <summary>
        ///     Tipo da DANFE
        /// </summary>
        string TipoDanfe { get; }

        #endregion

        /// <summary>
        ///     Obter Danfe
        /// </summary>
        /// <returns></returns>
        Task<Documento> ObterDocHtmlAsync();
    }
}
