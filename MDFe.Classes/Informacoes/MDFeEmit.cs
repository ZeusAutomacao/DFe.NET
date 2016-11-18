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
using DFe.Classes.Extencoes;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeEmit
    {
        public MDFeEmit()
        {
            EnderEmit = new MDFeEnderEmit();
        }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "IE")]
        public string IE { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        [XmlElement(ElementName = "xFant")]
        public string XFant { get; set; }

        [XmlElement(ElementName = "enderEmit")]
        public MDFeEnderEmit EnderEmit { get; set; }
    }

    [Serializable]
    public class MDFeEnderEmit
    {
        [XmlElement(ElementName = "xLgr")]
        public string XLgr { get; set; }

        [XmlElement(ElementName = "nro")]
        public string Nro { get; set; }

        [XmlElement(ElementName = "xCpl")]
        public string XCpl { get; set; }

        [XmlElement(ElementName = "xBairro")]
        public string XBairro { get; set; }

        [XmlElement(ElementName = "cMun")]
        public long CMun { get; set; }

        [XmlElement(ElementName = "xMun")]
        public string XMun { get; set; }

        [XmlIgnore]
        public long CEP { get; set; }

        [XmlElement(ElementName = "CEP")]
        public string ProxyCEP
        {
            get { return CEP.ToString("D8"); }
            set { CEP = long.Parse(value); }
        }

        [XmlIgnore]
        public EstadoUF UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }

        [XmlElement(ElementName = "fone")]
        public string Fone { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
    }
}