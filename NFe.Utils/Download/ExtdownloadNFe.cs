using DFe.Utils;
using NFe.Classes.Servicos.Download;

namespace NFe.Utils.Download
{
    public static class ExtdownloadNFe
    {
        /// <summary>
        ///     Recebe um objeto ExtdownloadNFe e devolve a string no formato XML
        /// </summary>
        /// <param name="pedDownload">Objeto do tipo downloadNFe</param>
        /// <returns>string com XML no do objeto downloadNFe</returns>
        public static string ObterXmlString(this downloadNFe pedDownload)
        {
            return FuncoesXml.ClasseParaXmlString(pedDownload);
        }
    }
}