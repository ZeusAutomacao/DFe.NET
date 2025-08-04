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
    public class gMonoReten
    {
        private decimal _qBcMonoReten;
        private decimal _adRemIbsReten;
        private decimal _vIbsMonoReten;
        private decimal _adRemCbsReten;
        private decimal _vCbsMonoReten;
        
        /// <summary>
        ///     UB91 - Quantidade tributada sujeita à retenção na monofasia
        /// </summary>
        public decimal qBCMonoReten
        {
            get => _qBcMonoReten.Arredondar(4);
            set => _qBcMonoReten = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB92 - Alíquota ad rem do IBS sujeito a retenção
        /// </summary>
        public decimal adRemIBSReten
        {
            get => _adRemIbsReten.Arredondar(4);
            set => _adRemIbsReten = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB93 - Valor do IBS monofásico sujeito a retenção
        /// </summary>
        public decimal vIBSMonoReten
        {
            get => _vIbsMonoReten.Arredondar(2);
            set => _vIbsMonoReten = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB93a - Alíquota ad rem da CBS sujeito a retenção
        /// </summary>
        public decimal adRemCBSReten
        {
            get => _adRemCbsReten.Arredondar(4);
            set => _adRemCbsReten = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB93b - Valor da CBS monofásica sujeita a retenção
        /// </summary>
        public decimal vCBSMonoReten
        {
            get => _vCbsMonoReten.Arredondar(2);
            set => _vCbsMonoReten = value.Arredondar(2);
        }
    }
}