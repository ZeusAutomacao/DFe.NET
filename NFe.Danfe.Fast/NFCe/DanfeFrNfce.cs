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

using NFe.Classes;
using NFe.Danfe.Base.NFCe;
using Shared.DFe.Danfe.Fast;

namespace NFe.Danfe.Fast.NFCe
{
    /// <summary>
    /// Classe responsável pela impressão do DANFE da NFCe em Fast Reports
    /// </summary>
    public class DanfeFrNfce : DanfeBase
    {
        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFCe em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo nfeProc</param>
        /// <param name="configuracaoDanfeNfce">Objeto do tipo ConfiguracaoDanfeNfce contendo as definições de impressão</param>
        /// <param name="cIdToken">Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ</param>
        /// <param name="csc">Código de Segurança do Contribuinte(antigo Token)</param>
        /// <param name="arquivoRelatorio">Caminho e arquivo frx contendo as definições do relatório personalizado</param>
        /// <param name="textoRodape">Texto para ser impresso no final do documento</param>
        public DanfeFrNfce(nfeProc proc, ConfiguracaoDanfeNfce configuracaoDanfeNfce, string cIdToken, string csc, string arquivoRelatorio = "", string textoRodape = "")
        {
            Relatorio = DanfeSharedHelper.GenerateDanfeNfceReport(proc, configuracaoDanfeNfce, cIdToken, csc, Properties.Resources.NFCe, arquivoRelatorio);
        }

        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE da NFCe em Fast Reports.
        /// Use esse construtor apenas para impressão em contingência, já que neste modo ainda não é possível obter o grupo protNFe 
        /// </summary>
        /// <param name="nfe">Objeto do tipo NFe</param>
        /// <param name="configuracaoDanfeNfce">Objeto do tipo ConfiguracaoDanfeNfce contendo as definições de impressão</param>
        /// <param name="cIdToken">Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ</param>
        /// <param name="csc">Código de Segurança do Contribuinte(antigo Token)</param>
        /// <param name="textoRodape">Texto para ser impresso no final do documento</param>
        public DanfeFrNfce(Classes.NFe nfe, ConfiguracaoDanfeNfce configuracaoDanfeNfce, string cIdToken, string csc, string textoRodape = "") :
            this(new nfeProc() { NFe = nfe }, configuracaoDanfeNfce, cIdToken, csc, arquivoRelatorio: string.Empty, textoRodape: textoRodape)
        {
        }
    }
}
