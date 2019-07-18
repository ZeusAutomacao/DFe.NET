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
using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.Classes.Informacoes.Identificacao
{
    public class ide
    {

        public ide(ConfiguracaoServico configuracaoServico = null)
        {
            _configuracaoServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
        }
        
        public ide()
        {
            
        }

        [XmlIgnore] 
        private ConfiguracaoServico _configuracaoServico;

        /// <summary>
        ///     B02 - Código da UF do emitente do Documento Fiscal. Utilizar a Tabela do IBGE.
        /// </summary>
        public Estado cUF { get; set; }

        [XmlIgnore]
        public int cCT { get; set; }

        [XmlElement(ElementName = "cCT")]
        public string ProxycCT
        {
            get { return this.cCT.ToString("00000000"); }
            set { this.cCT = int.Parse(value); }
        }

        public int CFOP { get; set; }

        public string natOp { get; set; }

        /// <summary>
        /// Versão 2.00
        /// </summary>
        public forPag? forPag { get; set; }

        public bool forPagSpecified { get { return forPag.HasValue; } }

        /// <summary>
        ///     B06 - Modelo do Documento Fiscal
        /// </summary>
        public ModeloDocumento mod { get; set; }

        public short serie { get; set; }

        public long nCT { get; set; }

        /// <summary>
        /// Versão 3.0  AAAA-MM-DDTHH:MM:DD TZD
        /// Versão 2.0  AAAA-MM-DDTHH:MM:DD
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhEmi { get; set; }

        [XmlElement(ElementName = "dhEmi")]
        public string ProxydhEmi
        {
            get
            {
                if (_configuracaoServico == null)
                    _configuracaoServico = ConfiguracaoServico.Instancia;
                switch (_configuracaoServico.VersaoLayout)
                {
                    case versao.ve200:
                        return dhEmi.ParaDataHoraStringSemUtc();
                    case versao.ve300:
                        return dhEmi.ParaDataHoraStringUtc();
                    default:
                        throw new InvalidOperationException("Versão Inválida para CT-e");
                }
            }
            set { dhEmi = DateTimeOffset.Parse(value); }
        }

        public tpImp tpImp { get; set; }

        public tpEmis tpEmis { get; set; }

        public int cDV { get; set; }

        /// <summary>
        ///     B24 - Identificação do Ambiente
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     B11 - Tipo do Documento Fiscal
        /// </summary>
        public tpCTe tpCTe { get; set; }

        /// <summary>
        /// Versão 3.00 = Informe AplicativoContribuinte ou ContribuinteAplicativoFisco
        /// Versão 2.00 = Qualquer uma das opções
        /// </summary>
        public procEmi procEmi { get; set; }

        public string verProc { get; set; }

        /// <summary>
        /// Versão 3.00
        /// </summary>
        public byte? indGlobalizado { get; set; }

        /// <summary>
        /// Se null, não aparece no xml
        /// </summary>
        public bool indGlobalizadoSpecified { get { return indGlobalizado.HasValue; } }

        /// <summary>
        /// Versão 2.00
        /// </summary>
        public string refCTE { get; set; }

        public long cMunEnv { get; set; }

        public string xMunEnv { get; set; }

        [XmlIgnore]
        public Estado UFEnv { get; set; }

        [XmlElement(ElementName = "UFEnv")]
        public string ProxyUFEnv
        {
            get { return UFEnv.GetSiglaUfString(); }
            set { UFEnv = UFEnv.SiglaParaEstado(value); }
        }

        public modal modal { get; set; }

        public tpServ tpServ { get; set; }

        public long cMunIni { get; set; }

        public string xMunIni { get; set; }

        [XmlIgnore]
        public Estado UFIni { get; set; }

        [XmlElement(ElementName = "UFIni")]
        public string ProxyUFIni { get { return UFIni.GetSiglaUfString(); }
            set { UFIni = UFIni.SiglaParaEstado(value); } }

        public long cMunFim { get; set; }

        public string xMunFim { get; set; }

        [XmlIgnore]
        public Estado UFFim { get; set; }

        [XmlElement(ElementName = "UFFim")]
        public string ProxyUFFim { get { return UFFim.GetSiglaUfString(); } set
        {
            UFFim = UFFim.SiglaParaEstado(value);
        } }

        [XmlElement(ElementName = "infPercurso")]
        public List<infPercurso> infPercurso { get; set; }

        public retira retira { get; set; }

        public string xDetRetira { get; set; }

        /// <summary>
        /// Versao 3.00 é obrigatório
        /// </summary>
        public indIEToma? indIEToma { get; set; }

        /// <summary>
        /// Se null, não aparece no xml
        /// </summary>
        public bool indIETomaSpecified { get { return indIEToma.HasValue; } }

        /// <summary>
        /// Versão 2.00 = toma03
        /// Versão 3.00 = toma3
        /// </summary>
        [XmlElement("toma03", typeof(toma03))]
        [XmlElement("toma3", typeof(toma3))]
        public tomaBase3 tomaBase3 { get; set; }
        public toma4 toma4 { get; set; }

        [XmlIgnore]
        public DateTime? dhCont { get; set; }

        [XmlElement(ElementName = "dhCont")]
        public string ProxydhCont
        {
            get
            {
                if (dhCont == null) return null;

                return dhCont.Value.ParaDataHoraStringUtc();
            }
            set
            {
                dhCont = Convert.ToDateTime(value);
            }
        }

        public string xJust { get; set; }
    }
}
