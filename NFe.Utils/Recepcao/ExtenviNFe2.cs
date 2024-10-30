using DFe.Utils;
using NFe.Classes.Servicos.Recepcao;

namespace NFe.Utils.Recepcao
{
    public static class ExtenviNFe2
    {
        /// <summary>
        ///     Converte o objeto enviNFe2 para uma string no formato XML
        /// </summary>
        /// <param name="pedEnvio"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto enviNFe2</returns>
        public static string ObterXmlString(this enviNFe2 pedEnvio)
        {
            return FuncoesXml.ClasseParaXmlString(pedEnvio);
        }
    }
}