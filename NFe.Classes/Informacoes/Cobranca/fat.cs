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
namespace NFe.Classes.Informacoes.Cobranca
{
    public class fat
    {
        private decimal? _vOrig;
        private decimal? _vDesc;
        private decimal? _vLiq;

        /// <summary>
        ///     Y03 - Número da Fatura
        /// </summary>
        public string nFat { get; set; }

        /// <summary>
        ///     Y04 - Valor Original da Fatura
        /// </summary>
        public decimal? vOrig
        {
            get { return _vOrig.Arredondar(2); }
            set { _vOrig = value.Arredondar(2); }
        }

        /// <summary>
        ///     Y05 - Valor do desconto
        /// </summary>
        public decimal? vDesc
        {
            get { return _vDesc.Arredondar(2); }
            set { _vDesc = value.Arredondar(2); }
        }

        /// <summary>
        ///     Y06 - Valor Líquido da Fatura
        /// </summary>
        public decimal? vLiq
        {
            get { return _vLiq.Arredondar(2); }
            set { _vLiq = value.Arredondar(2); }
        }

        public bool ShouldSerializevOrig()
        {
            return vOrig.HasValue;
        }

        public bool ShouldSerializevDesc()
        {
            return vDesc.HasValue;
        }

        public bool ShouldSerializevLiq()
        {
            return vLiq.HasValue;
        }
    }
}