using CTe.Classes.Servicos.Recepcao.Retorno;
using DFe.Utils;

namespace CTe.Utils.Recepcao
{
    public static class ExtconsReciCTe
    {
        /// <summary>
        ///     Converte o objeto consReciCTe para uma string no formato XML
        /// </summary>
        /// <param name="pedRecibo"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto consReciCTe</returns>
        public static string ObterXmlString(this consReciCTe pedRecibo)
        {
            return FuncoesXml.ClasseParaXmlString(pedRecibo);
        }
    }
}