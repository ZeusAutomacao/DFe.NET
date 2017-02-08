using CTeDLL.Classes.Servicos.Inutilizacao;
using DFe.Utils;

namespace CTeDLL.Utils.Inutilizacao
{
    public static class ExtretInutCTe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retInutCTe
        /// </summary>
        /// <param name="retInutCTe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retInutCTe</returns>
        public static retInutCTe CarregarDeXmlString(this retInutCTe retInutCTe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retInutCTe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retInutCTe para uma string no formato XML
        /// </summary>
        /// <param name="retInutCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retInutCTe</returns>
        public static string ObterXmlString(this retInutCTe retInutCTe)
        {
            return FuncoesXml.ClasseParaXmlString(retInutCTe);
        }
    }
}