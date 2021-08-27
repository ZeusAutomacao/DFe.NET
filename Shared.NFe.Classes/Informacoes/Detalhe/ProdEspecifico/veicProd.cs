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
    public class veicProd : ProdutoEspecifico
    {
        /// <summary>
        ///     J02 - Tipo da operação
        /// </summary>
        
        public TipoOperacao tpOp { get; set; }

        /// <summary>
        ///     J03 - Chassi do veículo
        /// </summary>
        public string chassi { get; set; }

        /// <summary>
        ///     J04 - Cor(Código de cada montadora)
        /// </summary>
        public string cCor { get; set; }

        /// <summary>
        ///     J05 - Descrição da Cor
        /// </summary>
        public string xCor { get; set; }

        /// <summary>
        ///     J06 - Potência Motor (CV)
        /// </summary>
        public string pot { get; set; }

        /// <summary>
        ///     J07 - Cilindradas
        /// </summary>
        public string cilin { get; set; }

        /// <summary>
        ///     J08 - Peso Líquido
        /// </summary>
        public string pesoL { get; set; }

        /// <summary>
        ///     J09 - Peso Bruto
        /// </summary>
        public string pesoB { get; set; }

        /// <summary>
        ///     J10 - Serial (série)
        /// </summary>
        public string nSerie { get; set; }

        /// <summary>
        ///     J11 - Tipo de combustível. Utilizar Tabela RENAVAM (v2.0)
        /// </summary>
        public string tpComb { get; set; }

        /// <summary>
        ///     J12 - Número de Motor
        /// </summary>
        public string nMotor { get; set; }

        /// <summary>
        ///     J13 - Capacidade Máxima de Tração
        /// </summary>
        public string CMT { get; set; }

        /// <summary>
        ///     J14 - Distância entre eixos
        /// </summary>
        public string dist { get; set; }

        /// <summary>
        ///     J16 - Ano Modelo de Fabricação
        /// </summary>
        public int anoMod { get; set; }

        /// <summary>
        ///     J17 - Ano de Fabricação
        /// </summary>
        public int anoFab { get; set; }

        /// <summary>
        ///     J18 - Tipo de Pintura
        /// </summary>
        public string tpPint { get; set; }

        /// <summary>
        ///     J19 - Tipo de Veículo
        /// </summary>
        public string tpVeic { get; set; }

        /// <summary>
        ///     J20 - Espécie de Veículo
        /// </summary>
        public int espVeic { get; set; }

        /// <summary>
        ///     J21 - Condição do VIN
        /// </summary>
        public CondicaoVin VIN { get; set; }

        /// <summary>
        ///     J22 - Condição do Veículo
        /// </summary>
        public CondicaoVeiculo condVeic { get; set; }

        /// <summary>
        ///     J23 - Código Marca Modelo
        /// </summary>
        public string cMod { get; set; }

        /// <summary>
        ///     J24 - Código da Cor
        /// </summary>
        public string cCorDENATRAN { get; set; }

        /// <summary>
        ///     J25 - Capacidade máxima de lotação
        /// </summary>
        public int lota { get; set; }

        /// <summary>
        ///     J26 - Restrição
        /// </summary>
        public TipoRestricao tpRest { get; set; }
    }
}
