using DFe.Utils;
using NFe.Classes.Servicos.AdmCsc;

namespace NFe.Utils.AdmCsc
{
    public static class ExtAdmCscNFCe
    {
        /// <summary>
        ///     Converte o objeto admCscNFCe para uma string no formato XML
        /// </summary>
        /// <param name="admCscNFCe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto admCscNFCe</returns>
        public static string ObterXmlString(this admCscNFCe admCscNFCe)
        {
            return FuncoesXml.ClasseParaXmlString(admCscNFCe);
        }
    }
}