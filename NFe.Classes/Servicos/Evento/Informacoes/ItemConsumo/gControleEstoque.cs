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

namespace NFe.Classes.Servicos.Evento.Informacoes.ItemConsumo
{
    public class gControleEstoque
    {
        private decimal? _qConsumo;
        private decimal? _qtde;

        /// <summary>
        ///     P28 - Informar a quantidade para consumo de pessoa física.
        ///     Use para o evento do tipo "Destinação de item para consumo pessoal".
        /// </summary>
        public decimal? qConsumo
        {
            get => _qConsumo.Arredondar(4);
            set => _qConsumo = value.Arredondar(4);
        }
        
        /// <summary>
        ///     P28 - Informar a quantidade que não atendeu os requisitos para a conversão em isenção.
        ///     Use para o evento do tipo "Importação em ALC/ZFM não convertida em isenção".
        /// </summary>
        public decimal? qtde
        {
            get => _qtde.Arredondar(4);
            set => _qtde = value.Arredondar(4);
        }
        
        /// <summary>
        ///     P29 - Informar a unidade relativa ao campo gConsumo.
        ///     Use para o evento do tipo "Destinação de item para consumo pessoal".
        /// </summary>
        public string uConsumo { get; set; }
        
        /// <summary>
        ///     P29 - Informar a unidade relativa ao campo gConsumo.
        ///     Use para o evento do tipo "Importação em ALC/ZFM não convertida em isenção".
        /// </summary>
        public string unidade { get; set; }
        
        public bool ShouldSerializeqConsumo() => qConsumo.HasValue;
        
        public bool ShouldSerializeqtde() => qtde.HasValue;
    }
}