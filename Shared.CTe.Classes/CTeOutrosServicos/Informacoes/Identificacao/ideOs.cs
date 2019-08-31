using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Identificacao;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.Identificacao
{
    public class ideOs
    {
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
        ///     B06 - Modelo do Documento Fiscal
        /// </summary>
        public ModeloDocumento mod { get; set; }

        public short serie { get; set; }

        public long nCT { get; set; }

        /// <summary>
        /// Versão 3.0  AAAA-MM-DDTHH:MM:DD TZD
        /// </summary>
        [XmlIgnore]
        public DateTime dhEmi { get; set; }

        [XmlElement(ElementName = "dhEmi")]
        public string ProxydhEmi
        {
            get { return this.dhEmi.ParaDataHoraStringUtc(); }
            set { this.dhEmi = DateTime.Parse(value); }
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

        /// <summary>
        /// Unico permitido
        /// 01 - Rodoviário
        /// </summary>
        public modal modal { get; set; }

        /// <summary>
        /// Unicos Permitidos
        /// Transporte de Pessoas
        /// Transporte de Valores
        /// Excesso de Bagagem
        /// </summary>
        public tpServ tpServ { get; set; }

        public indIEToma indIEToma { get; set; }

        public long cMunIni { get; set; }

        public string xMunIni { get; set; }

        [XmlIgnore]
        public Estado UFIni { get; set; }

        [XmlElement(ElementName = "UFIni")]
        public string ProxyUFIni
        {
            get { return UFIni.GetSiglaUfString(); }
            set { UFIni = UFIni.SiglaParaEstado(value); }
        }

        public long cMunFim { get; set; }

        public string xMunFim { get; set; }

        [XmlIgnore]
        public Estado UFFim { get; set; }

        [XmlElement(ElementName = "UFFim")]
        public string ProxyUFFim
        {
            get { return UFFim.GetSiglaUfString(); }
            set
            {
                UFFim = UFFim.SiglaParaEstado(value);
            }
        }

        [XmlElement("infPercurso")]
        public List<infPercurso> infPercurso { get; set; }

        [XmlIgnore]
        public DateTime? dhCont { get; set; }

        [XmlElement(ElementName = "dhCont")]
        public string ProxydhCont
        {
            get
            {
                if (dhCont == null) return null;

                return dhCont.Value.ParaDataHoraString();
            }
            set
            {
                dhCont = Convert.ToDateTime(value);
            }
        }

        public string xJust { get; set; }
    }
}