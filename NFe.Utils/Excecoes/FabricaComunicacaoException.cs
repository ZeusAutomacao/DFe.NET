using System;
using System.Collections.Generic;
using System.Net;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Excecoes
{
    /// <summary>
    /// Classe responsável pelo tratamento da exceção ComunicacaoException
    /// </summary>
    public static class FabricaComunicacaoException
    {
        /// <summary>
        /// Obtém uma exceção.
        /// Se o status da <see cref="WebException"/> estiver na lista <see cref="ListaComunicacaoException"/>, 
        /// será retornada uma exceção do tipo <see cref="ComunicacaoException"/>, 
        /// senão será retornada a própria <see cref="WebException"/> passada no parâmetro
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="webException"></param>
        /// <returns></returns>
        public static Exception ObterException(ServicoNFe servico, WebException webException)
        {
            if (ListaComunicacaoException.Contains(webException.Status))
                return new ComunicacaoException(servico, webException.Message);
            return webException;
        }

        /// <summary>
        /// Lista com os status de WebException que serão traduzidos para ComunicacaoException
        /// </summary>
        private static readonly List<WebExceptionStatus> ListaComunicacaoException = new List<WebExceptionStatus>
        {
            WebExceptionStatus.CacheEntryNotFound,
            WebExceptionStatus.ConnectFailure,
            WebExceptionStatus.ConnectionClosed,
            WebExceptionStatus.KeepAliveFailure,
            WebExceptionStatus.MessageLengthLimitExceeded,
            WebExceptionStatus.NameResolutionFailure,
            WebExceptionStatus.Pending,
            WebExceptionStatus.PipelineFailure,
            //WebExceptionStatus.ProtocolError,
            WebExceptionStatus.ProxyNameResolutionFailure,
            WebExceptionStatus.ReceiveFailure,
            WebExceptionStatus.RequestCanceled,
            WebExceptionStatus.RequestProhibitedByCachePolicy,
            WebExceptionStatus.RequestProhibitedByProxy,
            //WebExceptionStatus.SecureChannelFailure,
            WebExceptionStatus.SendFailure,
            WebExceptionStatus.ServerProtocolViolation,
            //WebExceptionStatus.Success,
            WebExceptionStatus.Timeout,
            //WebExceptionStatus.TrustFailure,
            WebExceptionStatus.UnknownError
        };
    }
}