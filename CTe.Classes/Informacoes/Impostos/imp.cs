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
using CTe.Classes.Informacoes.Impostos.IBSCBS;
using CTe.Classes.Informacoes.Valores;
using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos
{
    public class imp
    {
        public Tributacao.ICMS ICMS { get; set; }

        private decimal? _vTotTrib;
        public decimal? vTotTrib
        {
            get { return _vTotTrib.Arredondar(2); }
            set { _vTotTrib = value.Arredondar(2); }
        }

        public bool vTotTribSpecified { get { return vTotTrib.HasValue; } }

        public string infAdFisco { get; set; }

        public ICMSUFFim ICMSUFFim { get; set; }

        public infTribFed infTribFed { get; set; }


        public Tributacao.IBSCBS IBSCBS { get; set; }

        private decimal? _vTotDFe;
        /// <summary>
        /// O total geral do DFe dever� ser a soma do total da presta��o + IBS + CBS
        ///     vTotDFe = vPrest / vTPrest + gIBSCBS / vIBS + gCBS / vCBS
        /// 
        /// Exce��o: Em 2026 n�o somar IBS e CBS
        /// Observa��o: Implementa��o futura
        /// </summary>
        public decimal? vTotDFe
        {
            get { return _vTotDFe.Arredondar(2); }
            set { _vTotDFe = value.Arredondar(2); }
        }

        public bool vTotDFeSpecified { get { return vTotDFe.HasValue; } }
    }
}