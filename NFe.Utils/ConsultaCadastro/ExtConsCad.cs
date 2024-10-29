using DFe.Utils;
using NFe.Classes.Servicos.ConsultaCadastro;

namespace NFe.Utils.ConsultaCadastro
{
    public static class ExtConsCad
    {
        /// <summary>
        ///     Converte o objeto ConsCad para uma string no formato XML
        /// </summary>
        /// <param name="pedConsulta"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto ConsCad</returns>
        public static string ObterXmlString(this ConsCad pedConsulta)
        {
            return FuncoesXml.ClasseParaXmlString(pedConsulta);
        }
    }
}