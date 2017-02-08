using CTeDLL.Classes.Servicos.Evento;
using DFe.Utils;

namespace CTeDLL.Utils.Evento
{
    public static class ExtretEnvEvento
    {
        /// <summary>
        ///     Carrega um objeto do tipo retEnvEvento a partir de uma string no formato XML
        /// </summary>
        /// <param name="retEnvEvento"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto retEnvEvento com as informações da string XML</returns>
        public static retEnvEvento CarregarDeXmlString(this retEnvEvento retEnvEvento, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retEnvEvento>(xmlString);
        }

        /// <summary>
        ///     Converte um objeto do tipo retEnvEvento para uma string no formato XML com os dados do objeto
        /// </summary>
        /// <param name="retEnvEvento"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retEnvEvento</returns>
        public static string ObterXmlString(this retEnvEvento retEnvEvento)
        {
            return FuncoesXml.ClasseParaXmlString(retEnvEvento);
        }
    }
}