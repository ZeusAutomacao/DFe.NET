/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emiss�o de Nota Fiscal Eletr�nica - NFe e Nota Fiscal de  */
/* Consumidor Eletr�nica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Voc� pode obter a �ltima vers�o desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca � software livre; voc� pode redistribu�-la e/ou modific�-la */
/* sob os termos da Licen�a P�blica Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a vers�o 2.1 da Licen�a, ou (a seu crit�rio) */
/* qualquer vers�o posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca � distribu�da na expectativa de que seja �til, por�m, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia impl�cita de COMERCIABILIDADE OU      */
/* ADEQUA��O A UMA FINALIDADE ESPEC�FICA. Consulte a Licen�a P�blica Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICEN�A.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Voc� deve ter recebido uma c�pia da Licen�a P�blica Geral Menor do GNU junto*/
/* com esta biblioteca; se n�o, escreva para a Free Software Foundation, Inc.,  */
/* no endere�o 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Voc� tamb�m pode obter uma copia da licen�a em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco jos� da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using System.IO;

namespace NFe.Danfe.Base
{
    public interface IDanfe
    {
        /// <summary>
        /// Envia a impress�o do DANFE da NFCe diretamente para a impressora
        /// </summary>
        /// <param name="exibirDialogo">Se true exibe o di�logo Imprimindo...</param>
        /// <param name="impressora">Passe a string com o nome da impressora para imprimir diretamente em determinada impressora. Caso contr�rio, a impress�o ser� feita na impressora que estiver como padr�o</param>
        void Imprimir(bool exibirDialogo = true, string impressora = "");

        /// <summary>
        /// Converte o DANFE para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DANFE</param>
        void ExportarPdf(string arquivo);

        void ExportarPdf(Stream outputStream);
    }
}