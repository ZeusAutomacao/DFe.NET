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

namespace NFe.Classes.Servicos.Evento.Informacoes.ItemConsumo
{
    public class gConsumo
    {
        private decimal _vIbs;
        private decimal _vCbs;
        
        /// <summary>
        ///     P24 - Corresponde ao atributo “nItem” do elemento “det” da NF-e de aquisição
        /// </summary>
        [XmlAttribute("nItem")]
        public int nItem { get; set; }

        /// <summary>
        ///     P25 - Valor do IBS na nota de aquisição correspondente à quantidade destinada a uso e consumo pessoal
        /// </summary>
        [XmlAttribute("vIBS")]
        public decimal vIBS
        {
            get => _vIbs.Arredondar(2);
            set => _vIbs = value.Arredondar(2);
        }
        
        /// <summary>
        ///     P26 - Valor da CBS na nota de aquisição correspondente à quantidade destinada a uso e consumo pessoal
        /// </summary>
        [XmlAttribute("vCBS")]
        public decimal vCBS
        {
            get => _vCbs.Arredondar(2);
            set => _vCbs = value.Arredondar(2);
        }
        
        /// <summary>
        ///     P27 - Informações de quantidade de estoque influenciadas pelo evento
        /// </summary>
        public gControleEstoque gControleEstoque { get; set; }
        
        /// <summary>
        ///     P31 - Informar a chave da nota (NFe ou NFCe) emitida para o fornecimento nos casos em que a legislação obriga a emissão de documento fiscal
        /// </summary>
        public string refNF { get; set; }
        
        /// <summary>
        ///     P32 - Corresponde ao “nItem” da refNFe 
        /// </summary>
        public int nItemRefNFe { get; set; }
    }
}