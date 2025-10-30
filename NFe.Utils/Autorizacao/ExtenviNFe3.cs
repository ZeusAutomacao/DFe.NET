using DFe.Utils;
using NFe.Classes.Servicos.Autorizacao;

namespace NFe.Utils.Autorizacao
{
    public static class ExtenviNFe3
    {
        /// <summary>
        ///     Converte o objeto enviNFe3 para uma string no formato XML
        /// </summary>
        /// <param name="pedEnvio"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto enviNFe3</returns>
        public static string ObterXmlString(this enviNFe3 pedEnvio)
        {
            return FuncoesXml.ClasseParaXmlString(pedEnvio);
        }
    }
}