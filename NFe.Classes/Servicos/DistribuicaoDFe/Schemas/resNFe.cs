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
using System.ComponentModel;
using System.Xml.Serialization;
using DFe.Utils;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class resNFe
    {
        /// <summary>
        /// C02 - Versão do leiaute
        /// </summary>
        [XmlAttribute()]
        public decimal versao { get; set; }

        /// <summary>
        /// C03 - Chave de acesso da NF-e
        /// </summary>
        [XmlElement(DataType = "integer")]
        public string chNFe { get; set; }

        /// <summary>
        /// C04 - CNPJ do Emitente
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// C05 - CPF do Emitente
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// C06 - Razão Social ou Nome do Emitente
        /// </summary>
        public string xNome { get; set; }

        /// <summary>
        /// C07 - IE do Emitente. Valores válidos: 
        /// vazio  (não contribuinte do ICMS), 
        /// ISENTO (contribuinte do ICMS ISENTO de Inscrição no Cadastro de Contribuintes) ou 
        /// IE (Contribuinte do ICMS)
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        /// C08 - Data de Emissão da NF-e
        /// </summary>
        [XmlIgnore]
        public DateTime dhEmi { get; set; }

        /// <summary>
        /// Proxy para dhEmi no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhEmi")]
        public string ProxyDhEmi
        {
            get { return dhEmi.ParaDataHoraStringUtc(); }
            set { dhEmi = DateTime.Parse(value); }
        }

        /// <summary>
        /// C09 - Tipo de Operação da NF-e: 0=Entrada; 1=Saída
        /// </summary>
        public byte tpNF { get; set; }

        /// <summary>
        /// C10 - Valor Total da NF-e
        /// </summary>
        public decimal vNF { get; set; }

        /// <summary>
        /// C11 - Digest Value da NF-e na base de dados do Ambiente Nacional
        /// </summary>
        public string digVal { get; set; }

        /// <summary>
        /// C12 - Data de autorização da NF-e
        /// </summary>
        public DateTime dhRecbto { get; set; }

        /// <summary>
        /// C13 - Número de protocolo da NF-e
        /// </summary>
        public ulong nProt { get; set; }

        /// <summary>
        /// C14 - Situação da NF-e: 1=Uso autorizado; 2=Uso denegado.
        /// </summary>
        public byte cSitNFe { get; set; }
    }
}
