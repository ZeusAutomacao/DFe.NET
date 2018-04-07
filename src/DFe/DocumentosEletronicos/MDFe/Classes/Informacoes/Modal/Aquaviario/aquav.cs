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
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Modal.Aquaviario
{
    [Serializable]
    public class aquav : ModalContainer
    {
        public string irin { get; set; }
        /// <summary>
        /// 1 - CNPJ da Agência de Navegação
        /// </summary>
        [XmlElement(ElementName = "CNPJAgeNav")]
        public string CNPJAgeNav { get; set; }

        /// <summary>
        /// 1 - Código do tipo de embarcação 
        /// </summary>
        [XmlElement(ElementName = "tpEmb")]
        public byte tpEmb { get; set; }

        /// <summary>
        /// 1 - Código da embarcação
        /// </summary>
        [XmlElement(ElementName = "cEmbar")]
        public string cEmbar { get; set; }

        /// <summary>
        /// 1 - Nome da embarcação 
        /// </summary>
        [XmlElement(ElementName = "xEmbar")]
        public string xEmbar { get; set; }

        /// <summary>
        /// 1 - Número da Viagem 
        /// </summary>
        [XmlElement(ElementName = "nViag")]
        public string nViag { get; set; }

        /// <summary>
        /// 1 - Código do Porto de Embarque 
        /// </summary>
        [XmlElement(ElementName = "cPrtEmb")]
        public string cPrtEmb { get; set; }

        /// <summary>
        /// 1 - Código do Porto de Destino 
        /// </summary>
        [XmlElement(ElementName = "cPrtDest")]
        public string cPrtDest { get; set; }

        public string prtTrans { get; set; }

        public tpNav? tpNav { get; set; }

        public bool tpNavSpecified { get { return tpNav.HasValue; } }

        /// <summary>
        /// 1 - Grupo de informações dos terminais de carregamento.
        /// </summary>
        [XmlElement(ElementName = "infTermCarreg")]
        public List<infTermCarreg> infTermCarreg { get; set; }

        /// <summary>
        /// 1 - Grupo de informações dos terminais de descarregamento.
        /// </summary>
        [XmlElement(ElementName = "infTermDescarreg")]
        public List<infTermDescarreg> infTermDescarreg { get; set; }

        /// <summary>
        /// 1 - Informações das Embarcações do Comboio
        /// </summary>
        [XmlElement(ElementName = "infEmbComb")]
        public List<infEmbComb> infEmbComb { get; set; }

        /// <summary>
        /// 1 - Informações das Undades de Carga vazias
        /// </summary>
        [XmlElement(ElementName = "infUnidCargaVazia")]
        public List<infUnidTranspVazia> infUnidCargaVazia { get; set; }

        public List<infUnidCargaVazia> infUnidTranspVazia { get; set; }
    }
}