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
using NFe.Danfe.Base.NFe;
using Shared.DFe.Danfe.Fast;

namespace NFe.Danfe.Fast.Standard.NFe
{
    /// <summary>
    /// Classe responsável pela impressão do DANFE dos eventos da NFe, em Fast Reports
    /// </summary>
    public class DanfeFrEvento : DanfeBase
    {
        /// <summary>
        /// Construtor da classe responsável pela impressão do DANFE do evento da NFe, em Fast Reports
        /// </summary>
        /// <param name="proc">Objeto do tipo <see cref="nfeProc"/></param>
        /// <param name="procEventoNFe">Objeto do tipo <see cref="Classes.Servicos.Consulta.procEventoNFe"/></param>
        /// <param name="configuracaoDanfeNfe">Objeto do tipo <see cref="ConfiguracaoDanfeNfe"/> contendo as definições de impressão</param>
        /// <param name="desenvolvedor">Texto do desenvolvedor a ser informado no DANFE</param>
        public DanfeFrEvento(nfeProc proc, Classes.Servicos.Consulta.procEventoNFe procEventoNFe, ConfiguracaoDanfeNfe configuracaoDanfeNfe, string desenvolvedor = "")
        {
            Relatorio = DanfeSharedHelper.GenerateDanfeFrEventoReport(proc, procEventoNFe, configuracaoDanfeNfe, Properties.Resources.NFeEvento, desenvolvedor);
        }
    }
}
