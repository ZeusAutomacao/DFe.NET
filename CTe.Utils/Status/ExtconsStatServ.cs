using CTeDLL.Classes.Servicos.Status;
using DFe.Utils;

namespace CTeDLL.Utils.Status
{
    public static class ExtconsStatServ
    {
        /// <summary>
        ///     Recebe um objeto consStatServ e devolve a string no formato XML
        /// </summary>
        /// <param name="pedStatus">Objeto do tipo consStatServ</param>
        /// <returns>string com XML no do objeto consStatServ</returns>
        public static string ObterXmlString(this consStatServCte pedStatus)
        {
            return FuncoesXml.ClasseParaXmlString(pedStatus);
        }
    }
}