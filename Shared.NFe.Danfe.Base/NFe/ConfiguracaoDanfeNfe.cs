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

namespace NFe.Danfe.Base.NFe
{
    public enum ImprimirUnidQtdeValor
    {
        Comercial,
        Tributavel,
        Ambos
    }

    public class ConfiguracaoDanfeNfe : ConfiguracaoDanfe
    {
        public ConfiguracaoDanfeNfe(byte[] logomarca = null, bool duasLinhas = true, bool documentoCancelado = false, bool quebrarLinhasObservacao = false) : this()
        {
            Logomarca = logomarca;
            DuasLinhas = duasLinhas;
            DocumentoCancelado = documentoCancelado;
            QuebrarLinhasObservacao = quebrarLinhasObservacao;            
        }

        /// <summary>
        /// Construtor sem parâmetros para serialização
        /// </summary>
        private ConfiguracaoDanfeNfe()
        {
            Logomarca = null;
            DuasLinhas = true;
            DocumentoCancelado = false;
            QuebrarLinhasObservacao = true;
            ExibirResumoCanhoto = true;
            ResumoCanhoto = string.Empty;
            ChaveContingencia = string.Empty;
            ExibeCampoFatura = false;
            ImprimirISSQN = true;
            ImprimirDescPorc = false;
            ImprimirTotalLiquido = false;
            ImprimirUnidQtdeValor = ImprimirUnidQtdeValor.Comercial;
            ExibirTotalTributos = false;
            DecimaisValorUnitario = 2;
            DecimaisQuantidadeItem = 2;
            DataHoraImpressao = null;
        }

        public bool DuasLinhas { get; set; }

        public bool QuebrarLinhasObservacao { get; set; }

        public bool ExibeCampoFatura { get; set; }

        public bool ExibirResumoCanhoto { get; set; }

        public string ResumoCanhoto { get; set; }

        public string ChaveContingencia { get; set; }

        public bool ImprimirISSQN { get; set; }

        public bool ImprimirDescPorc { get; set; }

        public bool ImprimirTotalLiquido { get; set; }

        public ImprimirUnidQtdeValor ImprimirUnidQtdeValor { get; set; }

        public bool ExibirTotalTributos { get; set; }

        public int DecimaisValorUnitario { get; set; }

        public int DecimaisQuantidadeItem { get; set; }

        public DateTime? DataHoraImpressao { get; set; }
    }
}