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
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeIde
    {
        public MDFeIde()
        {
            InfMunCarrega = new List<MDFeInfMunCarrega>();
            InfPercurso = new List<MDFeInfPercurso>();
        }

        [XmlElement(ElementName = "cUF")]
        public EstadoUF CUF { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "tpEmit")]
        public MDFeTipoEmitente TpEmit { get; set; }

        [XmlElement(ElementName = "mod")]
        public MDFeModelo Mod { get; set; }

        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        [XmlElement(ElementName = "nMDF")]
        public long NMDF { get; set; }

        [XmlElement(ElementName = "cMDF")]
        public long CMDF { get; set; }

        [XmlElement(ElementName = "cDV")]
        public byte CDV { get; set; }

        [XmlElement(ElementName = "modal")]
        public MDFeModal Modal { get; set; }

        [XmlIgnore]
        public DateTime DhEmi { get; set; }

        [XmlElement(ElementName = "dhEmi")]
        public string ProxyDhEmi
        {
            get { return DhEmi.ToString("yyyy-MM-ddTHH:mm:dd"); }
            set { DhEmi = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "tpEmis")]
        public MDFeTipoEmissao TpEmis { get; set; }

        [XmlElement(ElementName = "procEmi")]
        public MDFeIdentificacaoProcessoEmissao ProcEmi { get; set; }

        [XmlElement(ElementName = "verProc")]
        public string VerProc { get; set; }

        [XmlIgnore]
        public EstadoUF UFIni { get; set; }

        [XmlElement(ElementName = "UFIni")]
        public string ProxyUFIni
        {
            get { return UFIni.GetSiglaUfString(); }
            set { UFIni = UFIni.SiglaParaEstado(value); }
        }

        [XmlIgnore]
        public EstadoUF UFFim { get; set; }

        [XmlElement(ElementName = "UFFim")]
        public string ProxyUFFim
        {
            get { return UFFim.GetSiglaUfString(); }
            set { UFFim = UFFim.SiglaParaEstado(value); }
        }

        [XmlElement(ElementName = "infMunCarrega")]
        public List<MDFeInfMunCarrega> InfMunCarrega { get; set; }

        [XmlElement(ElementName = "infPercurso")]
        public List<MDFeInfPercurso> InfPercurso { get; set; }

        [XmlIgnore]
        public DateTime? DhIniViagem { get; set; }

        [XmlElement(ElementName = "dhIniViagem")]
        public string ProxyDhIniViagem {
            get { return DhIniViagem?.ToString("yyyy-MM-ddTHH:mm:dd"); }
            set { DhIniViagem = DateTime.Parse(value); }
        }
    }
}