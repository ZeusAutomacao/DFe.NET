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
using CTe.Classes.Ext;
using CTe.Classes.Informacoes;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Assinatura;
using DFe.Utils;

namespace CTe.Classes
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class CTe
    {
        /// <summary>
        /// CTe Modelo 67/ CTE Ordem de Serviço
        /// CTeOs
        /// </summary>
        [XmlIgnore]
        public versao? versao { get; set; }

        [XmlAttribute(AttributeName = "versao")]
        public string ProxyVersao
        {
            get
            {
                if (versao == null) return null;
                return versao.Value.GetString();
            }
            set
            {
                if(value.Equals("2.00"))
                    versao = Servicos.Tipos.versao.ve200;

                if(value.Equals("3.00"))
                    versao = Servicos.Tipos.versao.ve300;
            }
        }

        public bool versaoSpecified { get { return versao.HasValue; } }

        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
        public infCte infCte { get; set; }

        public infCTeSupl infCTeSupl { get; set; }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        public static CTe LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<CTe>(xml);
        }

        public static CTe LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<CTe>(caminhoArquivoXml);
        }
    }
}