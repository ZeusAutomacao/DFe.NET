using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.Identificacao
{
    public class ide
    {
        private readonly ConfiguracaoServico _configuracaoServico = ConfiguracaoServico.Instancia;

        /// <summary>
        ///     B02 - Código da UF do emitente do Documento Fiscal. Utilizar a Tabela do IBGE.
        /// </summary>
        public Estado cUF { get; set; }

        public string cCT { get; set; }

        public int CFOP { get; set; }

        public string natOp { get; set; }

        /// <summary>
        /// Versão 2.00
        /// </summary>
        public forPag forPag { get; set; }

        /// <summary>
        ///     B06 - Modelo do Documento Fiscal
        /// </summary>
        public ModeloDocumento mod { get; set; }

        public short serie { get; set; }

        public int nCT { get; set; }

        /// <summary>
        /// Versão 3.0  AAAA-MM-DDTHH:MM:DD TZD
        /// Versão 2.0  AAAA-MM-DDTHH:MM:DD
        /// </summary>
        [XmlIgnore]
        public DateTime dhEmi { get; set; }

        [XmlElement(ElementName = "dhEmi")]
        public string ProxydhEmi
        {
            get
            {
                switch (_configuracaoServico.VersaoServico)
                {
                    case versao.ve200:
                        return dhEmi.ParaDataHoraStringSemUtc();
                    case versao.ve300:
                        return dhEmi.ParaDataHoraStringUtc();
                    default:
                        throw new InvalidOperationException("Versão Inválida para CT-e");
                }
            }
            set { dhEmi = DateTime.Parse(value); }
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

        public string cMunEnv { get; set; }

        public string xMunEnv { get; set; }

        public Estado UFEnv { get; set; }

        public modal modal { get; set; }

        public tpServ tpServ { get; set; }

        public string cMunIni { get; set; }

        public string xMunIni { get; set; }

        public Estado UFIni { get; set; }

        public string cMunFim { get; set; }

        public string xMunFim { get; set; }

        public Estado UFFim { get; set; }

        public retira retira { get; set; }

        public string xDetRetira { get; set; }

        /// <summary>
        /// Versao 3.00 é obrigatório
        /// </summary>
        public indIEToma? IndIeToma { get; set; }

        /// <summary>
        /// Se null, não aparece no xml
        /// </summary>
        public bool IndIeTomaSpecified { get { return IndIeToma.HasValue; } }

        /// <summary>
        /// Versão 2.00 = toma03
        /// Versão 3.00 = toma3
        /// </summary>
        [XmlElement("toma03", typeof(toma03))]
        [XmlElement("toma3", typeof(toma3))]
        public tomaBase3 tomaBase3 { get; set; }
        public toma4 toma4 { get; set; }

        public string dhCont { get; set; }

        public string xJust { get; set; }
    }
}
