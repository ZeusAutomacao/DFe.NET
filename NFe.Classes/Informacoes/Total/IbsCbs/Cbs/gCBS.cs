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

namespace NFe.Classes.Informacoes.Total.IbsCbs.Cbs
{
    public class gCBS
    {
        private decimal _vDif;
        private decimal _vDevTrib;
        private decimal _vCBS;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;
        
        /// <summary>
        ///     W53 - Valor total do diferimento
        /// </summary>
        public decimal vDif
        {
            get => _vDif;
            set => _vDif = value.Arredondar(2);
        }
        
        /// <summary>
        ///     W54 - Valor total de devolução de tributos
        /// </summary>
        public decimal vDevTrib
        {
            get => _vDevTrib;
            set => _vDevTrib = value.Arredondar(2);
        }
        
        /// <summary>
        ///     W56 - Valor total do CBS
        /// </summary>
        public decimal vCBS
        {
            get => _vCBS;
            set => _vCBS = value.Arredondar(2);
        }

        /// <summary>
        ///     W56a - Valor total do crédito presumido
        /// </summary>
        public decimal vCredPres
        {
            get => _vCredPres;
            set => _vCredPres = value.Arredondar(2);
        }

        /// <summary>
        ///     W56b - Valor total do crédito presumido em condição suspensiva
        /// </summary>
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus;
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}