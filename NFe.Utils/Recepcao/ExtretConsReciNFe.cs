using DFe.Utils;
using NFe.Classes.Servicos.Recepcao.Retorno;

namespace NFe.Utils.Recepcao
{
    public static class ExtretConsReciNFe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retConsReciNFe
        /// </summary>
        /// <param name="retConsReciNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retConsReciNFe</returns>
        public static retConsReciNFe CarregarDeXmlString(this retConsReciNFe retConsReciNFe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retConsReciNFe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retConsReciNFe para uma string no formato XML
        /// </summary>
        /// <param name="retConsReciNFe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retConsReciNFe</returns>
        public static string ObterXmlString(this retConsReciNFe retConsReciNFe)
        {
            return FuncoesXml.ClasseParaXmlString(retConsReciNFe);
        }
    }
}