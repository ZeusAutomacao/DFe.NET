using CTeDLL.Classes.Servicos.Recepcao;
using DFe.Utils;

namespace CTeDLL.Utils.Recepcao
{
    public static class ExtretEnviCTe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retEnviCTe
        /// </summary>
        /// <param name="retEnviCTe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retEnviCTe</returns>
        public static retEnviCTe CarregarDeXmlString(this retEnviCTe retEnviCTe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retEnviCTe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retEnviCTe para uma string no formato XML
        /// </summary>
        /// <param name="retEnviCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retEnviCTe</returns>
        public static string ObterXmlString(this retEnviCTe retEnviCTe)
        {
            return FuncoesXml.ClasseParaXmlString(retEnviCTe);
        }
    }
}