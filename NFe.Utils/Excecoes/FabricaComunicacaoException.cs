/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

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
