using DFe.Utils;
using NFe.Classes.Servicos.Consulta;

namespace NFe.Utils.Consulta
{
    public static class ExtconsSitNFe
    {
        /// <summary>
        ///     Converte o objeto consSitNFe para uma string no formato XML
        /// </summary>
        /// <param name="pedConsulta"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto consSitNFe</returns>
        public static string ObterXmlString(this consSitNFe pedConsulta)
        {
            return FuncoesXml.ClasseParaXmlString(pedConsulta);
        }
    }
}