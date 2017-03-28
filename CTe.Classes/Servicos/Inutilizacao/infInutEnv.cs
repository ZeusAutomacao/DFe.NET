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
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace CTe.Classes.Servicos.Inutilizacao
{
    public class infInutEnv
    {
        public infInutEnv()
        {
            xServ = "INUTILIZAR";
        }

        /// <summary>
        ///     DP04 - Identificador da TAG a ser assinada formada com Código da UF + Ano (2 posições) + CNPJ + modelo + série +
        ///     nro inicial e nro final precedida do literal “ID”
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        ///     DP05 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     DP06 - Serviço solicitado: "INUTILIZAR"
        /// </summary>
        public string xServ { get; set; }

        /// <summary>
        ///     DP07 - Código da UF do solicitante
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     DP08 - Ano de inutilização da numeração
        /// </summary>
        public int ano { get; set; }

        /// <summary>
        ///     DP09 - CNPJ do emitente
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        ///     DP10 - Modelo da NF-e (55)
        /// </summary>
        public ModeloDocumento mod { get; set; }

        /// <summary>
        ///     DP11 - Série da NF-e
        /// </summary>
        public short serie { get; set; }

        /// <summary>
        ///     DP12 - Número da NF-e inicial a ser inutilizada
        /// </summary>
        public long nCTIni { get; set; }

        /// <summary>
        ///     DP13 - Número da NF-e final a ser inutilizada
        /// </summary>
        public long nCTFin { get; set; }

        /// <summary>
        ///     DP14 - Informar a justificativa do pedido de inutilização
        /// </summary>
        public string xJust { get; set; }
    }
}