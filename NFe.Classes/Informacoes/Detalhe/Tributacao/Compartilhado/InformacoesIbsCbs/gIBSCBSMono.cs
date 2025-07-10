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

using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs
{
    public class gIBSCBSMono
    {
        private decimal _qBcMono;
        private decimal _adRemIbs;
        private decimal _adRemCbs;
        private decimal _vIbsMono;
        private decimal _vCbsMono;
        private decimal _qBcMonoReten;
        private decimal _adRemIbsReten;
        private decimal _vIbsMonoReten;
        private decimal _adRemCbsReten;
        private decimal _vCbsMonoReten;
        private decimal _qBcMonoRet;
        private decimal _adRemIbsRet;
        private decimal _vIbsMonoRet;
        private decimal _adRemCbsRet;
        private decimal _vCbsMonoRet;
        private decimal _pDifIbs;
        private decimal _vIbsMonoDif;
        private decimal _pDifCbs;
        private decimal _vCbsMonoDif;
        private decimal _vTotIbsMonoItem;
        private decimal _vTotCbsMonoItem;

        /// <summary>
        ///     UB85 - Quantidade tributada na monofasia
        /// </summary>
        [XmlElement(Order = 1)]
        public decimal qBCMono
        {
            get => _qBcMono.Arredondar(4);
            set => _qBcMono = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB86 - Alíquota ad rem do IBS
        /// </summary>
        [XmlElement(Order = 2)]
        public decimal adRemIBS
        {
            get => _adRemIbs.Arredondar(4);
            set => _adRemIbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB87 - Alíquota ad rem da CBS
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal adRemCBS
        {
            get => _adRemCbs.Arredondar(4);
            set => _adRemCbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB88 - Valor do IBS monofásico
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal vIBSMono
        {
            get => _vIbsMono.Arredondar(2);
            set => _vIbsMono = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB89 - Valor da CBS monofásica
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal vCBSMono
        {
            get => _vCbsMono.Arredondar(2);
            set => _vCbsMono = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB91 - Quantidade tributada sujeita à retenção na monofasia
        /// </summary>
        [XmlElement(Order = 6)]
        public decimal qBCMonoReten
        {
            get => _qBcMonoReten.Arredondar(4);
            set => _qBcMonoReten = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB92 - Alíquota ad rem do IBS sujeito a retenção
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal adRemIBSReten
        {
            get => _adRemIbsReten.Arredondar(4);
            set => _adRemIbsReten = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB93 - Valor do IBS monofásico sujeito a retenção
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal vIBSMonoReten
        {
            get => _vIbsMonoReten.Arredondar(2);
            set => _vIbsMonoReten = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB93a - Alíquota ad rem da CBS sujeito a retenção
        /// </summary>
        [XmlElement(Order = 9)]
        public decimal adRemCBSReten
        {
            get => _adRemCbsReten.Arredondar(4);
            set => _adRemCbsReten = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB93b - Valor da CBS monofásica sujeita a retenção
        /// </summary>
        [XmlElement(Order = 10)]
        public decimal vCBSMonoReten
        {
            get => _vCbsMonoReten.Arredondar(2);
            set => _vCbsMonoReten = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB95 - Quantidade tributada retida anteriormente
        /// </summary>
        [XmlElement(Order = 11)]
        public decimal qBCMonoRet
        {
            get => _qBcMonoRet.Arredondar(4);
            set => _qBcMonoRet = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB96 - Alíquota ad rem do IBS retido anteriormente
        /// </summary>
        [XmlElement(Order = 12)]
        public decimal adRemIBSRet
        {
            get => _adRemIbsRet.Arredondar(4);
            set => _adRemIbsRet = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB97 - Valor do IBS retido anteriormente
        /// </summary>
        [XmlElement(Order = 13)]
        public decimal vIBSMonoRet
        {
            get => _vIbsMonoRet.Arredondar(2);
            set => _vIbsMonoRet = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB98 - Alíquota ad rem da CBS retida anteriormente
        /// </summary>
        [XmlElement(Order = 14)]
        public decimal adRemCBSRet
        {
            get => _adRemCbsRet.Arredondar(4);
            set => _adRemCbsRet = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB98a - Valor da CBS retida anteriormente
        /// </summary>
        [XmlElement(Order = 15)]
        public decimal vCBSMonoRet
        {
            get => _vCbsMonoRet.Arredondar(2);
            set => _vCbsMonoRet = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB100 - Percentual do diferimento do imposto monofásico
        /// </summary>
        [XmlElement(Order = 16)]
        public decimal pDifIBS
        {
            get => _pDifIbs.Arredondar(4);
            set => _pDifIbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB101 - Valor do IBS monofásico diferido
        /// </summary>
        [XmlElement(Order = 17)]
        public decimal vIBSMonoDif
        {
            get => _vIbsMonoDif.Arredondar(2);
            set => _vIbsMonoDif = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB102 - Percentual do diferimento do imposto monofásico
        /// </summary>
        [XmlElement(Order = 18)]
        public decimal pDifCBS
        {
            get => _pDifCbs.Arredondar(4);
            set => _pDifCbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB103 - Valor da CBS Monofásica diferida
        /// </summary>
        [XmlElement(Order = 19)]
        public decimal vCBSMonoDif
        {
            get => _vCbsMonoDif.Arredondar(2);
            set => _vCbsMonoDif = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB104 - Total de IBS Monofásico
        /// </summary>
        [XmlElement(Order = 20)]
        public decimal vTotIBSMonoItem
        {
            get => _vTotIbsMonoItem.Arredondar(2);
            set => _vTotIbsMonoItem = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB105 - Total da CBS Monofásica
        /// </summary>
        [XmlElement(Order = 21)]
        public decimal vTotCBSMonoItem
        {
            get => _vTotCbsMonoItem.Arredondar(2);
            set => _vTotCbsMonoItem = value.Arredondar(2);
        }
    }
}