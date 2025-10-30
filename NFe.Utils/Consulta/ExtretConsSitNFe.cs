using DFe.Utils;
using NFe.Classes.Servicos.Consulta;

namespace NFe.Utils.Consulta
{
    public static class ExtretConsSitNFe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retConsSitNFe
        /// </summary>
        /// <param name="retConsSitNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retConsSitNFe</returns>
        public static retConsSitNFe CarregarDeXmlString(this retConsSitNFe retConsSitNFe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retConsSitNFe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retConsSitNFe para uma string no formato XML
        /// </summary>
        /// <param name="retConsSitNFe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retConsSitNFe</returns>
        public static string ObterXmlString(this retConsSitNFe retConsSitNFe)
        {
            return FuncoesXml.ClasseParaXmlString(retConsSitNFe);
        }

        /// <summary>
        ///     Verifica se esta autorizado
        /// </summary>
        /// <param name="consSitNFe"></param>
        /// <returns>bool</returns>
        public static bool Autorizada(this retConsSitNFe consSitNFe)
        {
            return NfeSituacao.Autorizada(consSitNFe.cStat);
        }
    }
}