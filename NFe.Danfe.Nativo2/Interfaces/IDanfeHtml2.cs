using System.Threading.Tasks;
using NFe.Danfe.Nativo2.Dominio;

namespace NFe.Danfe.Nativo2.Interfaces
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
