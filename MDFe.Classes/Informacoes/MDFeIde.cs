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
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;
using MDFe.Classes.Flags;
using MDFe.Utils.Configuracoes;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeIde
    {
        public MDFeIde()
        {
            InfMunCarrega = new List<MDFeInfMunCarrega>();
            InfPercurso = new List<MDFeInfPercurso>();
        }

        /// <summary>
        /// 2 - Código da UF do emitente do MDF-e. 
        /// </summary>
        [XmlElement(ElementName = "cUF")]
        public Estado CUF { get; set; }

        /// <summary>
        /// 2 - Tipo do Ambiente 
        /// </summary>
        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        /// <summary>
        /// 2 - Tipo do Emitente 
        /// </summary>
        [XmlElement(ElementName = "tpEmit")]
        public MDFeTipoEmitente TpEmit { get; set; }

        /// <summary>
        /// MDF-e 3.0
        /// Tipo do Transportador
        /// Opcional
        /// </summary>
        [XmlElement(ElementName = "tpTransp")]
        public MDFeTpTransp? TpTransp { get; set; }

        public bool TpTranspSpecified { get { return TpTransp.HasValue; } }

        /// <summary>
        /// 2 - Modelo do Manifesto Eletrônico
        /// </summary>
        [XmlElement(ElementName = "mod")]
        public ModeloDocumento Mod { get; set; }

        /// <summary>
        /// 2- Série do Manifesto
        /// </summary>
        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        /// <summary>
        /// 2- Número do Manifesto 
        /// </summary>
        [XmlElement(ElementName = "nMDF")]
        public long NMDF { get; set; }

        /// <summary>
        /// 2 - Código numérico que compõe a Chave de Acesso. 
        /// </summary>
        [XmlIgnore]
        public int CMDF { get; set; }

        /// <summary>
        /// Proxy para cMDF
        /// </summary>
        [XmlElement(ElementName = "cMDF")]
        public string ProxyCMDF
        {
            get { return CMDF.ToString("00000000"); }
            set { CMDF = int.Parse(value); }
        }

        /// <summary>
        /// 2 - Digito verificador da chave de acesso do Manifesto
        /// </summary>
        [XmlElement(ElementName = "cDV")]
        public byte CDV { get; set; }

        /// <summary>
        /// 2 - Modalidade de transporte 
        /// </summary>
        [XmlElement(ElementName = "modal")]
        public MDFeModal Modal { get; set; }

        /// <summary>
        /// 2 - Data e hora de emissão do Manifesto 
        /// </summary>
        [XmlIgnore]
        public DateTime DhEmi { get; set; }

        /// <summary>
        /// Proxy para dhEmi
        /// </summary>
        [XmlElement(ElementName = "dhEmi")]
        public string ProxyDhEmi
        {
            get
            {
                switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        return DhEmi.ParaDataHoraStringSemUtc();
                    case VersaoServico.Versao300:
                        return DhEmi.ParaDataHoraStringUtc();
                    default:
                        throw new InvalidOperationException("Versão Inválida para MDF-e");
                }

            }
            set { DhEmi = DateTime.Parse(value); }
        }

        /// <summary>
        /// 2 - Forma de emissão do Manifesto (Normal ou Contingência)
        /// </summary>
        [XmlElement(ElementName = "tpEmis")]
        public MDFeTipoEmissao TpEmis { get; set; }

        /// <summary>
        /// 2 - Identificação do processo de emissão do Manifesto
        /// </summary>
        [XmlElement(ElementName = "procEmi")]
        public MDFeIdentificacaoProcessoEmissao ProcEmi { get; set; }

        /// <summary>
        /// 2 - Versão do processo de emissão 
        /// </summary>
        [XmlElement(ElementName = "verProc")]
        public string VerProc { get; set; }

        /// <summary>
        /// 2 - Sigla da UF do Carregamento 
        /// </summary>
        [XmlIgnore]
        public Estado UFIni { get; set; }

        /// <summary>
        /// Proxy para UFIni
        /// </summary>
        [XmlElement(ElementName = "UFIni")]
        public string ProxyUFIni
        {
            get { return UFIni.GetSiglaUfString(); }
            set { UFIni = UFIni.SiglaParaEstado(value); }
        }

        /// <summary>
        /// 2 - Sigla da UF do Descarregamento
        /// </summary>
        [XmlIgnore]
        public Estado UFFim { get; set; }

        /// <summary>
        /// Proxy para UFFim
        /// </summary>
        [XmlElement(ElementName = "UFFim")]
        public string ProxyUFFim
        {
            get { return UFFim.GetSiglaUfString(); }
            set { UFFim = UFFim.SiglaParaEstado(value); }
        }

        /// <summary>
        /// 2 - Informações dos Municípios de Carregamento
        /// </summary>
        [XmlElement(ElementName = "infMunCarrega")]
        public List<MDFeInfMunCarrega> InfMunCarrega { get; set; }

        /// <summary>
        /// 2 - Informações do Percurso do MDF-e 
        /// </summary>
        [XmlElement(ElementName = "infPercurso")]
        public List<MDFeInfPercurso> InfPercurso { get; set; }

        /// <summary>
        /// 2 - Data e hora previstos de inicio da viagem
        /// </summary>
        [XmlIgnore]
        public DateTime? DhIniViagem { get; set; }

        /// <summary>
        /// Proxy para dhIniViagem
        /// </summary>
        [XmlElement(ElementName = "dhIniViagem")]
        public string ProxyDhIniViagem { get; set; }

        [XmlElement(ElementName = "indCanalVerde")]
        public string IndCanalVerde { get; set; }

        [XmlElement(ElementName = "indCarregaPosterior")]
        public string IndCarregaPosterior { get; set; }
    }
}
