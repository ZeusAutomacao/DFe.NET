using CTeDLL.Classes.Servicos.Recepcao;
using DFe.Utils;

namespace CTeDLL.Utils.Recepcao
{
    public static class ExtenviCTe
    {
        /// <summary>
        ///     Converte o objeto enviCTe para uma string no formato XML
        /// </summary>
        /// <param name="pedEnvio"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto enviCTe</returns>
        public static string ObterXmlString(this enviCTe pedEnvio)
        {
            return FuncoesXml.ClasseParaXmlString(pedEnvio);
        }
    }
}