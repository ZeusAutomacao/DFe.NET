using System;
using CTeDLL.Classes.Servicos.Evento;
using DFe.Utils;

namespace CTeDLL.Utils.Evento
{
    public static class Extevento
    {
        /// <summary>
        ///     Converte o objeto evento para uma string no formato XML
        /// </summary>
        /// <param name="pedEvento"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto evento</returns>
        public static string ObterXmlString(this evento pedEvento)
        {
            return FuncoesXml.ClasseParaXmlString(pedEvento);
        }

        /// <summary>
        ///     Assina um objeto evento
        /// </summary>
        /// <param name="evento"></param>
        /// <returns>Retorna um objeto do tipo evento assinado</returns>
        public static evento Assina(this evento evento)
        {
            var eventoLocal = evento;
            if (eventoLocal.infEvento.Id == null)
                throw new Exception("Não é possível assinar um objeto evento sem sua respectiva Id!");

            // todo implementar
           /* var assinatura = Assinador.ObterAssinatura(eventoLocal, eventoLocal.infEvento.Id);
            eventoLocal.Signature = assinatura;*/
            return eventoLocal;
        }
    }
}