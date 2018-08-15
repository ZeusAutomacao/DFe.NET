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
namespace NFe.Classes.Informacoes.Detalhe.ProdEspecifico
{
    public class encerrante
    {
        private decimal _vEncIni;
        private decimal _vEncFin;

        /// <summary>
        /// LA12 - Número de identificação do bico utilizado no abastecimento
        /// </summary>
        public int nBico { get; set; }

        /// <summary>
        /// LA13 - Número de identificação da bomba ao qual o bico está interligado
        /// </summary>
        public int? nBomba { get; set; }
        public bool ShouldSerializenBomba()
        {
            return nBomba.HasValue;
        }

        /// <summary>
        /// LA14 - Número de identificação do tanque ao qual o bico está interligado
        /// </summary>
        public int nTanque { get; set; }

        /// <summary>
        /// LA15 - Valor do Encerrante no início do abastecimento
        /// </summary>
        public decimal vEncIni
        {
            get { return _vEncIni; }
            set { _vEncIni = value.Arredondar(3); }
        }

        /// <summary>
        /// LA16 - Valor do Encerrante no final do abastecimento
        /// </summary>
        public decimal vEncFin
        {
            get { return _vEncFin; }
            set { _vEncFin = value.Arredondar(3); }
        }
    }
}