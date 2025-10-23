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
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs.InformacoesIbs;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado
{
    public class IBSCBS
    {
        /// <summary>
        ///     UB13 - Código de Situação Tributária do IBS e CBS
        /// </summary>
        [XmlElement(Order = 1)]
        public CST CST { get; set; }
        
        /// <summary>
        ///     UB14 - Código de Classificação Tributária do IBS e CBS
        /// </summary>
        [XmlElement(Order = 2)]
        public string cClassTrib { get; set; }
        
        /// <summary>
        ///     UB14a - Indica a natureza da operação de doação, orientando a apuração e a geração de débitos ou estornos conforme o cenário
        /// </summary>
        [XmlElement(Order = 3)]
        public string indDoacao { get; set; }
        
        /// <summary>
        ///     UB15 - Grupo de Informações do IBS e da CBS
        /// </summary>
        [XmlElement(Order = 4)]
        public gIBSCBS gIBSCBS { get; set; }
        
        /// <summary>
        ///     UB84 - Grupo de Informações do IBS e CBS em operações com imposto monofásico
        /// </summary>
        [XmlElement(Order = 5)]
        public gIBSCBSMono gIBSCBSMono { get; set; }
        
        /// <summary>
        ///     UB106 - Transferências de Crédito
        /// </summary>
        [XmlElement(Order = 6)]
        public gTransfCred gTransfCred { get; set; }
        
        /// <summary>
        ///     UB112 - Ajuste de Competência
        /// </summary>
        [XmlElement(Order = 7)]
        public gAjusteCompet gAjusteCompet { get; set; }
        
        /// <summary>
        ///     UB116 - Estorno de Crédito
        /// </summary>
        [XmlElement(Order = 8)]
        public gEstornoCred gEstornoCred { get; set; }
        
        /// <summary>
        ///     UB120 - Crédito Presumido da Operação
        /// </summary>
        [XmlElement(Order = 9)]
        public gCredPresOper gCredPresOper { get; set; }
        
        /// <summary>
        ///     UB131 - Grupo para apropriação de crédito presumido de IBS sobre o saldo devedor na ZFM (art. 450, § 1º, LC 214/25)
        /// </summary>
        [XmlElement(Order = 10)]
        public gCredPresIBSZFM gCredPresIBSZFM { get; set; }
    }
}