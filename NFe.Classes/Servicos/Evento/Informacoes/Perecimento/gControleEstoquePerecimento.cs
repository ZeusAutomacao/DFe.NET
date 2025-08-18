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

namespace NFe.Classes.Servicos.Evento.Informacoes.Perecimento
{
    public class gControleEstoquePerecimento
    {
        private decimal _qPerecimento;
        private decimal? _vIBS;
        private decimal? _vCBS;

        /// <summary>
        ///     P28 - Informar a quantidade que foi objeto de roubo, perda, furto ou perecimento
        /// </summary>
        [XmlElement(Order = 1)]
        public decimal qPerecimento
        {
            get => _qPerecimento.Arredondar(4);
            set => _qPerecimento = value.Arredondar(4);
        }
        
        /// <summary>
        ///     P29 - Informar a unidade relativa ao campo qPerecimento
        /// </summary>
        [XmlElement(Order = 2)]
        public string uPerecimento { get; set; }
        
        /// <summary>
        ///     P31 - Valor do crédito IBS referente às aquisições a ser estornado correspondente à quantidade que foi objeto de roubo, perda, furto ou perecimento
        ///     <para>Evento: perecimento, perda, roubo ou furto durante o transporte contratado pelo fornecedor</para>
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal? vIBS
        {
            get => _vIBS; 
            set => _vIBS = value.Arredondar(2);
        }
        
        /// <summary>
        ///     P32 - Valor do crédito CBS referente às aquisições a ser estornado correspondente à quantidade que foi objeto de roubo, perda, furto ou perecimento
        ///     <para>Evento: perecimento, perda, roubo ou furto durante o transporte contratado pelo fornecedor</para>
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal? vCBS
        {
            get => _vCBS;
            set => _vCBS = value.Arredondar(2);
        }

        public bool ShouldSerializevIBS() => vIBS.HasValue;
        
        public bool ShouldSerializevCBS() => vCBS.HasValue;
    }
}