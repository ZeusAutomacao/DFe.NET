using CTeDLL.Classes.Servicos.Evento;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtevCCeCTe
    {
        /// <summary>
        ///     Converte o objeto evento para uma string no formato XML
        /// </summary>
        /// <param name="eventoCancelamento"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto evento</returns>
        public static string ObterXmlString(this evCCeCTe evCCeCTe)
        {
            return FuncoesXml.ClasseParaXmlString(evCCeCTe);
        }
    }
}