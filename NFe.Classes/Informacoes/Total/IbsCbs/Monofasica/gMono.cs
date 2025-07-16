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

namespace NFe.Classes.Informacoes.Total.IbsCbs.Monofasica
{
    public class gMono
    {
        private decimal _vIBSMono;
        private decimal _vCBSMono;
        private decimal _vIBSMonoReten;
        private decimal _vCBSMonoReten;
        private decimal _vIBSMonoRet;
        private decimal _vCBSMonoRet;

        /// <summary>
        ///     W58 - Total do IBS monofásico
        /// </summary>
        public decimal vIBSMono
        {
            get => _vIBSMono;
            set => _vIBSMono = value.Arredondar(2);
        }

        /// <summary>
        ///     W59 - Total da CBS monofásica
        /// </summary>
        public decimal vCBSMono
        {
            get => _vCBSMono;
            set => _vCBSMono = value.Arredondar(2);
        }

        /// <summary>
        ///     W59a - Total do IBS monofásico sujeito a retenção
        /// </summary>
        public decimal vIBSMonoReten
        {
            get => _vIBSMonoReten;
            set => _vIBSMonoReten = value.Arredondar(2);
        }

        /// <summary>
        ///     W59b - Total da CBS monofásica sujeita a retenção
        /// </summary>
        public decimal vCBSMonoReten
        {
            get => _vCBSMonoReten;
            set => _vCBSMonoReten = value.Arredondar(2);
        }

        /// <summary>
        ///     W59c - Total do IBS monofásico retido anteriormente
        /// </summary>
        public decimal vIBSMonoRet
        {
            get => _vIBSMonoRet;
            set => _vIBSMonoRet = value.Arredondar(2);
        }

        /// <summary>
        ///     W59d - Total da CBS monofásica retida anteriormente
        /// </summary>
        public decimal vCBSMonoRet
        {
            get => _vCBSMonoRet;
            set => _vCBSMonoRet = value.Arredondar(2);
        }
    }
}