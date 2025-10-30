using System;
using System.Security.Cryptography.X509Certificates;
using DFe.Utils;
using NFe.Classes.Servicos.Evento;
using NFe.Utils.Assinatura;

namespace NFe.Utils.Evento
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
        /// <param name="certificadoDigital">Informe o certificado digital, se já possuir esse em cache, evitando novo acesso ao certificado</param>
        /// <param name="signatureMethodSignedXml"></param>
        /// <param name="digestMethodReference"></param>
        /// <param name="removerAcentos"></param>
        /// <returns>Retorna um objeto do tipo evento assinado</returns>
        public static evento Assina(this evento evento, X509Certificate2 certificadoDigital,
            string signatureMethodSignedXml = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
            string digestMethodReference = "http://www.w3.org/2000/09/xmldsig#sha1", bool removerAcentos = false)
        {
            var eventoLocal = evento;
            if (eventoLocal.infEvento.Id == null)
                throw new Exception("Não é possível assinar um objeto evento sem sua respectiva Id!");

            var assinatura = Assinador.ObterAssinatura(eventoLocal, eventoLocal.infEvento.Id, certificadoDigital, false, signatureMethodSignedXml, digestMethodReference, removerAcentos);
            eventoLocal.Signature = assinatura;
            return eventoLocal;
        }
    }
}