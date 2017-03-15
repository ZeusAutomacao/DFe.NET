﻿/********************************************************************************/
/* Projeto: Biblioteca ZeusMDFe                                                 */
/* Biblioteca C# para emissão de Manifesto Eletrônico Fiscal de Documentos      */
/* (https://mdfe-portal.sefaz.rs.gov.br/                                        */
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
using MDFe.Classes.Contratos;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeAereo : MDFeModalContainer
    {
        /// <summary>
        /// 1 - Marca da Nacionalidade da aeronave 
        /// </summary>
        [XmlElement(ElementName = "nac")]
        public string Nac { get; set; }

        /// <summary>
        /// 1 - Marca de Matrícula da aeronave 
        /// </summary>
        [XmlElement(ElementName = "matr")]
        public string Matr { get; set; }

        /// <summary>
        /// 1 - Número do Voo 
        /// </summary>
        [XmlElement(ElementName = "nVoo")]
        public string NVoo { get; set; }

        /// <summary>
        /// 1 - Aeródromo de Embarque 
        /// </summary>
        [XmlElement(ElementName = "cAerEmb")]
        public string CAerEmb { get; set; }

        /// <summary>
        /// 1 - Aeródromo de Destino 
        /// </summary>
        [XmlElement(ElementName = "cAerDes")]
        public string CAerDes { get; set; }

        /// <summary>
        /// 1 - Data do Voo 
        /// </summary>
        [XmlIgnore]
        public DateTime DVoo { get; set; }

        /// <summary>
        /// Proxy para converter DVoo em string yyyy-MM-dd
        /// </summary>
        [XmlElement(ElementName = "dVoo")]
        public string ProxyDVoo
        {
            get { return DVoo.ParaDataString(); }
            set { DVoo = DateTime.Parse(value); }
        }
    }
}