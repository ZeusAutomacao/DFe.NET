using DFe.Utils;
using CTe.Classes.Servicos.DistribuicaoDFe;

namespace CTe.Utils.DistribuicaoDFe
{
    public static class ExtretDistDFeInt
    {
        /// <summary>
        /// Carrega um objeto do tipo retDistDFeInt a partir de uma string no formato XML
        /// </summary>
        /// <param name="retDistDFeInt">Objeto do tipo retDistDFeInt</param>
        /// <param name="xmlString">String com uma estrutura XML</param>
        /// <returns>Retorna um objeto retDistDFeInt com as informações da string XML</returns>
        public static retDistDFeInt CarregarDeXmlString(this retDistDFeInt retDistDFeInt, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retDistDFeInt>(xmlString);
        }

        /// <summary>
        /// Converte um objeto do tipo retDistDFeInt para uma string no formato XML com os dados do objeto
        /// </summary>
        /// <param name="retDistDFeInt">Objeto do tipo retDistDFeInt</param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retDistDFeInt</returns>
        public static string ObterXmlString(this retDistDFeInt retDistDFeInt)
        {
            return FuncoesXml.ClasseParaXmlString(retDistDFeInt);
        }
    }
}