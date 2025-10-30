using CTe.Classes.Servicos.Status;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtretConsStatServ
    {
        /// <summary>
        ///     Carrega um objeto do tipo retConsStatServ a partir de uma string no formato XML
        /// </summary>
        /// <param name="retConsStatServ"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto retConsStatServ com as informações da string XML</returns>
        public static retConsStatServCte CarregarDeXmlString(this retConsStatServCte retConsStatServ, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retConsStatServCte>(xmlString);
        }

        /// <summary>
        ///     Converte um objeto do tipo retConsStatServ para uma string no formato XML com os dados do objeto
        /// </summary>
        /// <param name="retConsStatServ"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retConsStatServ</returns>
        public static string ObterXmlString(this retConsStatServCte retConsStatServ)
        {
            return FuncoesXml.ClasseParaXmlString(retConsStatServ);
        }
    }
}