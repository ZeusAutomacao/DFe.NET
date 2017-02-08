using CTeDLL.Classes.Servicos.Consulta;
using DFe.Utils;

namespace CTeDLL.Utils.Consulta
{
    public static class ExtconsSitCTe
    {
        /// <summary>
        ///     Converte o objeto consSitCTe para uma string no formato XML
        /// </summary>
        /// <param name="pedConsulta"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto consSitCTe</returns>
        public static string ObterXmlString(this consSitCTe pedConsulta)
        {
            return FuncoesXml.ClasseParaXmlString(pedConsulta);
        }
    }
}