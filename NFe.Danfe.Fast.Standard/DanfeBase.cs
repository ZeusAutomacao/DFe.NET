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
using FastReport;
//using FastReport.Export.Pdf;
using NFe.Danfe.Base;

namespace NFe.Danfe.Fast.Standard
{
    public class DanfeBase: IDanfe
    {
        public Report Relatorio { get; protected set; }

        /// <summary>
        /// Abre a janela de visualiza��o do DANFE da NFCe
        /// </summary>
        /// <param name="modal">Se true, exibe a visualiza��o em Modal. O modo modal est� dispon�vel apenas para WinForms</param>
        public void Visualizar(bool modal = true)
        {
            Relatorio.Show(modal);
        }

        /// <summary>
        ///  Abre a janela de visualiza��o do design do DANFE da NFCe.
        /// Chame esse m�todo se desja fazer altera��es no design do DANFE em modo run-time
        /// </summary>
        /// <param name="modal">Se true, exibe a visualiza��o em Modal. O modo modal est� dispon�vel apenas para WinForms</param>
        public void ExibirDesign(bool modal = false)
        {
            Relatorio.Design(modal);
        }

        /// <summary>
        /// Envia a impress�o do DANFE da NFCe diretamente para a impressora
        /// </summary>
        /// <param name="exibirDialogo">Se true exibe o di�logo Imprimindo...</param>
        /// <param name="impressora">Passe a string com o nome da impressora para imprimir diretamente em determinada impressora. Caso contr�rio, a impress�o ser� feita na impressora que estiver como padr�o</param>
        public void Imprimir(bool exibirDialogo = true, string impressora = "")
        {
            Relatorio.PrintSettings.ShowDialog = exibirDialogo;
            Relatorio.PrintSettings.Printer = impressora;
            Relatorio.Print();
        }

        /// <summary>
        /// Converte o DANFE para PDF e salva-o no caminho/arquivo indicado
        /// </summary>
        /// <param name="arquivo">Caminho/arquivo onde deve ser salvo o PDF do DANFE</param>
        public void ExportarPdf(string arquivo)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFExport(), arquivo);
        }

        public void ExportarPdf(Stream outputStream)
        {
            Relatorio.Prepare();
            Relatorio.Export(new PDFExport(), outputStream);
            outputStream.Position = 0;
        }
    }
}