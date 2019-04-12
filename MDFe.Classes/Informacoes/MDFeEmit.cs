/********************************************************************************/
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
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeEmit
    {
        public MDFeEmit()
        {
            EnderEmit = new MDFeEnderEmit();
        }

        /// <summary>
        /// 2 - CNPJ do emitente 
        /// </summary>
        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        /// <summary>
        /// 2 - Inscrição Estadual do emitemte
        /// </summary>
        [XmlElement(ElementName = "IE")]
        public string IE { get; set; }

        /// <summary>
        /// 2 - Razão social ou Nome do emitente 
        /// </summary>
        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        /// <summary>
        /// 2 - Nome fantasia do emitente 
        /// </summary>
        [XmlElement(ElementName = "xFant")]
        public string XFant { get; set; }

        /// <summary>
        /// 2 - Endereço do emitente 
        /// </summary>
        [XmlElement(ElementName = "enderEmit")]
        public MDFeEnderEmit EnderEmit { get; set; }
    }

    [Serializable]
    public class MDFeEnderEmit
    {
        /// <summary>
        /// 3 - Logradouro
        /// </summary>
        [XmlElement(ElementName = "xLgr")]
        public string XLgr { get; set; }

        /// <summary>
        /// 3 - Número 
        /// </summary>
        [XmlElement(ElementName = "nro")]
        public string Nro { get; set; }

        /// <summary>
        /// 3 - Complemento
        /// </summary>
        [XmlElement(ElementName = "xCpl")]
        public string XCpl { get; set; }

        /// <summary>
        /// 3 - Bairro
        /// </summary>
        [XmlElement(ElementName = "xBairro")]
        public string XBairro { get; set; }

        /// <summary>
        /// 3 - Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.
        /// </summary>
        [XmlElement(ElementName = "cMun")]
        public long CMun { get; set; }

        /// <summary>
        /// 3 - Nome do município, , informar EXTERIOR para operações com o exterior
        /// </summary>
        [XmlElement(ElementName = "xMun")]
        public string XMun { get; set; }

        /// <summary>
        /// 3 - CEP
        /// </summary>
        [XmlIgnore]
        public long CEP { get; set; }

        /// <summary>
        /// Proxy para colocar zeros a esquerda no CEP 
        /// </summary>
        [XmlElement(ElementName = "CEP")]
        public string ProxyCEP
        {
            get { return CEP.ToString("D8"); }
            set { CEP = long.Parse(value); }
        }

        /// <summary>
        /// 3 - Sigla da UF, , informar EX para operações com o exterior.
        /// </summary>
        [XmlIgnore]
        public Estado UF { get; set; }

        /// <summary>
        /// Proxy para pegar SiglaUF do estado
        /// </summary>
        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }

        /// <summary>
        /// 3 - Telefone
        /// </summary>
        [XmlElement(ElementName = "fone")]
        public string Fone { get; set; }

        /// <summary>
        /// 3 - Endereço de E-mail 
        /// </summary>
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        public bool ShouldSerializeEmail()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}