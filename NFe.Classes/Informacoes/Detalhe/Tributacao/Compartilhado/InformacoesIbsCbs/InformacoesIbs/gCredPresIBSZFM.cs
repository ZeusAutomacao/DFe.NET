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

using System;
using System.Xml.Serialization;
using DFe.Utils;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs.InformacoesIbs
{
    public class gCredPresIBSZFM
    {
        private decimal? _vCredPresIbsZfm;

        /// <summary>
        /// UB132 - Ano e mês de referência do período de apuração (AAAA-MM)
        /// </summary>
        [XmlIgnore]
        public DateTime? competApur { get; set; }

        /// <summary>
        /// Proxy para competApur no formato AAAA-MM (somente ano e mês)
        /// </summary>
        [XmlElement(ElementName = "competApur")]
        public string ProxyCompetApur
        {
            get => competApur.ParaDataAnoEMesString();
            set => competApur = DateTime.Parse(value);
        }
        
        /// <summary>
        ///     UB133 - Tipo de classificação de acordo com o art. 450, § 1º, da LC 214/25 para o cálculo do crédito presumido na ZFM
        /// </summary>
        public ClassificacaoCreditoPresumidoIbsZfmTipos tpCredPresIBSZFM { get; set; }
        
        /// <summary>
        ///     UB134 - Valor do crédito presumido calculado sobre o saldo devedor apurado
        /// </summary>
        public decimal? vCredPresIBSZFM
        {
            get => _vCredPresIbsZfm.Arredondar(2);
            set => _vCredPresIbsZfm = value.Arredondar(2);
        }
        
        public bool ShouldSerializevCredPresIBSZFM() => vCredPresIBSZFM.HasValue;
    }
}