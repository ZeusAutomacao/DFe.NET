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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs
{
    public class gMonoPadrao
    {
        private decimal _qBcMono;
        private decimal _adRemIbs;
        private decimal _adRemCbs;
        private decimal _vIbsMono;
        private decimal _vCbsMono;
        
        /// <summary>
        ///     UB85 - Quantidade tributada na monofasia
        /// </summary>
        public decimal qBCMono
        {
            get => _qBcMono.Arredondar(4);
            set => _qBcMono = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB86 - Alíquota ad rem do IBS
        /// </summary>
        public decimal adRemIBS
        {
            get => _adRemIbs.Arredondar(4);
            set => _adRemIbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB87 - Alíquota ad rem da CBS
        /// </summary>
        public decimal adRemCBS
        {
            get => _adRemCbs.Arredondar(4);
            set => _adRemCbs = value.Arredondar(4);
        }

        /// <summary>
        ///     UB88 - Valor do IBS monofásico
        /// </summary>
        public decimal vIBSMono
        {
            get => _vIbsMono.Arredondar(2);
            set => _vIbsMono = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB89 - Valor da CBS monofásica
        /// </summary>
        public decimal vCBSMono
        {
            get => _vCbsMono.Arredondar(2);
            set => _vCbsMono = value.Arredondar(2);
        }
    }
}