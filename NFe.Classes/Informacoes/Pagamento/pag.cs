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

using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Pagamento
{
    public class pag
    {
        private decimal? _vPag;
        private decimal? _vTroco;

        /// <summary>
        /// YA01a - Grupo Detalhamento da Forma de Pagamento
        /// VERSÃO 4.00
        /// </summary>
        [XmlElement("detPag")]
        public List<detPag> detPag { get; set; }

        /// <summary>
        /// YA09 - Valor do troco
        /// Versão 4.00
        /// </summary>
        public decimal? vTroco
        {
            get { return _vTroco.Arredondar(2); }
            set { _vTroco = value.Arredondar(2); }
        }

        public bool vTrocoSpecified
        {
            get { return _vTroco.HasValue; }
        }

        /// <summary>
        ///     YA02 - Forma de pagamento
        ///     Versão 3.00
        /// </summary>
        public FormaPagamento? tPag { get; set; }

        public bool tPagSpecified
        {
            get { return tPag.HasValue; }
        }

        /// <summary>
        ///     YA03 - Valor do Pagamento
        /// Versão 3.00
        /// </summary>
        public decimal? vPag
        {
            get { return _vPag.Arredondar(2); }
            set { _vPag = value.Arredondar(2); }
        }

        public bool vPagSpecified
        {
            get { return vPag.HasValue; }
        }

        /// <summary>
        ///     YA04 - Grupo de Cartões
        ///     Versão 3.00
        /// </summary>
        public card card { get; set; }
    }
}