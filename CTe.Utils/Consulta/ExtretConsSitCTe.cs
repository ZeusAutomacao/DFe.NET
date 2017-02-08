using CTeDLL.Classes.Servicos.Consulta;
using DFe.Utils;

namespace CTeDLL.Utils.Consulta
{
    public static class ExtretConsSitCTe
    {
        /// <summary>
        ///     Coverte uma string XML no formato CTe para um objeto retConsSitCTe
        /// </summary>
        /// <param name="retConsSitCTe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retConsSitNFe</returns>
        public static retConsSitCTe CarregarDeXmlString(this retConsSitCTe retConsSitCTe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retConsSitCTe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retConsSitCTe para uma string no formato XML
        /// </summary>
        /// <param name="retConsSitCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retConsSitCTe</returns>
        public static string ObterXmlString(this retConsSitCTe retConsSitCTe)
        {
            return FuncoesXml.ClasseParaXmlString(retConsSitCTe);
        }
    }
}