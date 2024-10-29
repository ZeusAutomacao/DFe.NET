using DFe.Utils;
using NFe.Classes.Servicos.Inutilizacao;

namespace NFe.Utils.Inutilizacao
{
    public static class ExtretInutNFe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retInutNFe
        /// </summary>
        /// <param name="retInutNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retInutNFe</returns>
        public static retInutNFe CarregarDeXmlString(this retInutNFe retInutNFe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retInutNFe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retInutNFe para uma string no formato XML
        /// </summary>
        /// <param name="retInutNFe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retInutNFe</returns>
        public static string ObterXmlString(this retInutNFe retInutNFe)
        {
            return FuncoesXml.ClasseParaXmlString(retInutNFe);
        }
    }
}