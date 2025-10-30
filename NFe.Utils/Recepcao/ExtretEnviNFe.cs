using DFe.Utils;
using NFe.Classes.Servicos.Recepcao;

namespace NFe.Utils.Recepcao
{
    public static class ExtretEnviNFe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retEnviNFe
        /// </summary>
        /// <param name="retEnviNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retEnviNFe</returns>
        public static retEnviNFe CarregarDeXmlString(this retEnviNFe retEnviNFe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retEnviNFe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retEnviNFe para uma string no formato XML
        /// </summary>
        /// <param name="retEnviNFe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retEnviNFe</returns>
        public static string ObterXmlString(this retEnviNFe retEnviNFe)
        {
            return FuncoesXml.ClasseParaXmlString(retEnviNFe);
        }
    }
}