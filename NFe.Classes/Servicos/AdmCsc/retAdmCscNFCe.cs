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
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Servicos.AdmCsc
{
    /// <summary>
    /// AR01 - Estrutura com os dados de retorno para administrar o CSC
    /// </summary>
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot("retAdmCscNFCe",Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class retAdmCscNFCe : IRetornoServico
    {
        /// <summary>
        /// AR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        /// AR03 - Identificação do Ambiente: 1=Produção/2=Homologação 
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        /// AR04 - Identificador do tipo de operação: 1 - Consulta CSC Ativos / 2 - Solicita novo CSC / 3 - Revoga CSC Ativo
        /// </summary>
        public IdentificadorOperacaoCsc indOp { get; set; }

        /// <summary>
        /// AR05 - Código do resultado do processamento da solicitação
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        /// AR06 - Descrição literal do resultado do processamento da solicitação
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        /// AR07 - Tag de grupo para retorno dos dados de até dois CSC
        /// </summary>
        [XmlElement("dadosCsc", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public List<dadosCsc> dadosCsc { get; set; }
    }
}
