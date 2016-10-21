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
namespace NFe.Classes.Informacoes.Detalhe.Produto_Especifico
{
    public class med : ProdutoEspecifico
    {
        private decimal _qLote;
        private decimal _vPmc;

        /// <summary>
        ///     K02 - Número do Lote de medicamentos ou de matérias-primas farmacêuticas
        /// </summary>
        public string nLote { get; set; }

        /// <summary>
        ///     K03 - Quantidade de produto no Lote de medicamentos ou de matérias-primas farmacêuticas
        /// </summary>
        public decimal qLote
        {
            get { return _qLote; }
            set { _qLote = value.Arredondar(3); }
        }

        /// <summary>
        ///     K04 - Data de fabricação. Formato: “AAAA-MM-DD”
        /// </summary>
        public string dFab { get; set; }

        /// <summary>
        ///     K05 - Data de validade. Formato: “AAAA-MM-DD”
        /// </summary>
        public string dVal { get; set; }

        /// <summary>
        ///     K06 - Preço máximo consumidor
        /// </summary>
        public decimal vPMC
        {
            get { return _vPmc; }
            set { _vPmc = value.Arredondar(2); }
        }
    }
}