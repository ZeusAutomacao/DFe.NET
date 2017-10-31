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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMSUFDest
    {
        private decimal _vBcufDest;
        private decimal _pFcpufDest;
        private decimal _pIcmsufDest;
        private decimal _pIcmsInter;
        private decimal _pIcmsInterPart;
        private decimal _vFcpufDest;
        private decimal _vIcmsufDest;
        private decimal _vIcmsufRemet;

        /// <summary>
        /// NA03 - Valor da BC do ICMS na UF de destino
        /// </summary>
        public decimal vBCUFDest
        {
            get { return _vBcufDest; }
            set { _vBcufDest = value; }
        }

        /// <summary>
        /// NA05 - Percentual do ICMS relativo ao Fundo de Combate à Pobreza (FCP) na UF de destino
        /// </summary>
        public decimal pFCPUFDest
        {
            get { return _pFcpufDest; }
            set
            {
                _pFcpufDest = value; }
        }

        /// <summary>
        /// NA07 - Alíquota interna da UF de destino
        /// </summary>
        public decimal pICMSUFDest
        {
            get { return _pIcmsufDest; }
            set { _pIcmsufDest = value; }
        }

        /// <summary>
        /// NA09 - Alíquota interestadual das UF envolvidas
        /// </summary>
        public decimal pICMSInter
        {
            get { return _pIcmsInter; }
            set { _pIcmsInter = value; }
        }

        /// <summary>
        /// NA11 - Percentual provisório de partilha do ICMS Interestadual
        /// </summary>
        public decimal pICMSInterPart
        {
            get { return _pIcmsInterPart; }
            set { _pIcmsInterPart = value; }
        }

        /// <summary>
        /// NA13 - Valor do ICMS relativo ao Fundo de Combate à Pobreza(FCP) da UF de destino
        /// </summary>
        public decimal vFCPUFDest
        {
            get { return _vFcpufDest; }
            set { _vFcpufDest = value; }
        }

        /// <summary>
        /// NA15 - Valor do ICMS Interestadual para a UF de destino
        /// </summary>
        public decimal vICMSUFDest
        {
            get { return _vIcmsufDest; }
            set { _vIcmsufDest = value; }
        }

        /// <summary>
        /// NA17 - Valor do ICMS Interestadual para a UF do remetente
        /// </summary>
        public decimal vICMSUFRemet
        {
            get { return _vIcmsufRemet; }
            set { _vIcmsufRemet = value; }
        }
    }
}