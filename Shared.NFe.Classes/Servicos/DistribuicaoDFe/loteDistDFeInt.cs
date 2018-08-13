﻿/********************************************************************************/
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
using NFe.Classes.Servicos.DistribuicaoDFe.Schemas;

namespace NFe.Classes.Servicos.DistribuicaoDFe
{
    /// <summary>
    /// B10 - Conjunto de informações resumidas e documentos fiscais eletrônicos de interesse da pessoa ou empresa.
    /// </summary>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class loteDistDFeInt
    {
        /// <summary>
        /// B12 - NSU do documento fiscal
        /// </summary>
        [XmlAttribute()]
        public long NSU { get; set; }

        /// <summary>
        /// B13 - Identificação do Schema XML que será utilizado para validar o XML existente no campo seguinte.
        /// Vai identificar o tipo do documento e sua versão.
        /// Exemplos:
        /// - resNFe_v1.00.xsd
        /// - procNFe_v3.10.xsd
        /// - resEvento_1.00.xsd
        /// - procEventoNFe_v1.00.xsd
        /// </summary>
        [XmlAttribute()]
        public string schema { get; set; }

        /// <summary>
        /// Conteudo da Tag schema. 
        /// O conteúdo desta tag estará compactado no padrão gZip.O tipo do campo é base64Binary.
        /// </summary>
        [XmlText(DataType = "base64Binary")]
        public byte[] XmlNfe { get; set; }

        /// <summary>
        /// Resumo de NF-e 
        /// </summary>
        [XmlIgnore]
        public resNFe ResNFe { get; set; }

        /// <summary>
        /// Eventos da NFe
        /// </summary>
        [XmlIgnore]
        public procEventoNFe ProcEventoNFe { get; set; }

        /// <summary>
        /// Resumos de eventos
        /// </summary>
        [XmlIgnore]
        public resEvento ResEvento { get; set; }

        /// <summary>
        /// NF-e processada
        /// </summary>
        [XmlIgnore]
        public nfeProc NfeProc { get; set; }   
    }
}
