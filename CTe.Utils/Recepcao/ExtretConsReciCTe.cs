using CTeDLL.Classes.Servicos.Recepcao.Retorno;
using DFe.Utils;

namespace CTeDLL.Utils.Recepcao
{
    public static class ExtretConsReciCTe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retconsReciCTe
        /// </summary>
        /// <param name="retconsReciCTe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retConsReciCTe</returns>
        public static retconsReciCTe CarregarDeXmlString(this retconsReciCTe retConsReciCTe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retconsReciCTe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retconsReciCTe para uma string no formato XML
        /// </summary>
        /// <param name="retconsReciCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retconsReciCTe</returns>
        public static string ObterXmlString(this retconsReciCTe retConsReciCTe)
        {
            return FuncoesXml.ClasseParaXmlString(retConsReciCTe);
        }
    }
}