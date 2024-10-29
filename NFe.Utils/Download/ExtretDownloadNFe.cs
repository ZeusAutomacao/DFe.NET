using DFe.Utils;
using NFe.Classes.Servicos.Download;

namespace NFe.Utils.Download
{
    public static class ExtretDownloadNFe
    {
        /// <summary>
        ///     Carrega um objeto do tipo retDownloadNFe a partir de uma string no formato XML
        /// </summary>
        /// <param name="retDownloadNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto retDownloadNFe com as informações da string XML</returns>
        public static retDownloadNFe CarregarDeXmlString(this retDownloadNFe retDownloadNFe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retDownloadNFe>(xmlString);
        }

        /// <summary>
        ///     Converte um objeto do tipo retDownloadNFe para uma string no formato XML com os dados do objeto
        /// </summary>
        /// <param name="retDownloadNFe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retDownloadNFe</returns>
        public static string ObterXmlString(this retDownloadNFe retDownloadNFe)
        {
            return FuncoesXml.ClasseParaXmlString(retDownloadNFe);
        }
    }
}