using DFe.Utils;
using NFe.Classes.Servicos.Recepcao.Retorno;

namespace NFe.Utils.Recepcao
{
    public static class ExtconsReciNFe
    {
        /// <summary>
        ///     Converte o objeto consReciNFe para uma string no formato XML
        /// </summary>
        /// <param name="pedRecibo"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto consReciNFe</returns>
        public static string ObterXmlString(this consReciNFe pedRecibo)
        {
            return FuncoesXml.ClasseParaXmlString(pedRecibo);
        }
    }
}