using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MDFe.Classes.Contratos;
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeAquav : MDFeModalContainer
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
        public byte TpEmb { get; set; }

        /// <summary>
        /// 1 - Código da embarcação
        /// </summary>
        [XmlElement(ElementName = "cEmbar")]
        public string CEmbar { get; set; }

        /// <summary>
        /// 1 - Nome da embarcação 
        /// </summary>
        [XmlElement(ElementName = "xEmbar")]
        public string XEmbar { get; set; }

        /// <summary>
        /// 1 - Número da Viagem 
        /// </summary>
        [XmlElement(ElementName = "nViag")]
        public string NViag { get; set; }

        /// <summary>
        /// 1 - Código do Porto de Embarque 
        /// </summary>
        [XmlElement(ElementName = "cPrtEmb")]
        public string CPrtEmb { get; set; }

        /// <summary>
        /// 1 - Código do Porto de Destino 
        /// </summary>
        [XmlElement(ElementName = "cPrtDest")]
        public string CPrtDest { get; set; }

        public string prtTrans { get; set; }

        public tpNav? tpNav { get; set; }

        public bool tpNavSpecified { get { return tpNav.HasValue; } }

        /// <summary>
        /// 1 - Grupo de informações dos terminais de carregamento.
        /// </summary>
        [XmlElement(ElementName = "infTermCarreg")]
        public List<MDFeInfTermCarreg> InfTermCarregs { get; set; }

        /// <summary>
        /// 1 - Grupo de informações dos terminais de descarregamento.
        /// </summary>
        [XmlElement(ElementName = "infTermDescarreg")]
        public List<MDFeInfTermDescarreg> InfTermDescarregs { get; set; }

        /// <summary>
        /// 1 - Informações das Embarcações do Comboio
        /// </summary>
        [XmlElement(ElementName = "infEmbComb")]
        public List<MDFeInfEmbComb> InfEmbCombs { get; set; }

        /// <summary>
        /// 1 - Informações das Undades de Carga vazias
        /// </summary>
        [XmlElement(ElementName = "infUnidCargaVazia")]
        public List<MDFeInfUnidCargaVazia> InfUnidCargaVazias { get; set; }

        public List<infUnidTranspVazia> infUnidTranspVazia { get; set; }
    }
}