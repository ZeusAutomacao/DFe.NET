using DFe.Utils;
using NFe.Classes.Servicos.ConsultaCadastro;

namespace NFe.Utils.ConsultaCadastro
{
    public static class ExtretConsCad
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retConsCad
        /// </summary>
        /// <param name="retConsCad"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retConsCad</returns>
        public static retConsCad CarregarDeXmlString(this retConsCad retConsCad, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retConsCad>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retConsCad para uma string no formato XML
        /// </summary>
        /// <param name="retConsCad"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retConsCad</returns>
        public static string ObterXmlString(this retConsCad retConsCad)
        {
            return FuncoesXml.ClasseParaXmlString(retConsCad);
        }
    }
}