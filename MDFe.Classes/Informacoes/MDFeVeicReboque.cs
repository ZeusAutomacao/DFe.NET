using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeVeicReboque
    {
        /// <summary>
        /// 2 - Código interno do veículo
        /// </summary>
        [XmlElement(ElementName = "cInt")]
        public string CInt { get; set; }

        /// <summary>
        /// 2 - Placa do veículo 
        /// </summary>
        [XmlElement(ElementName = "placa")]
        public string Placa { get; set; }

        /// <summary>
        /// 2 - RENAVAM do veículo 
        /// </summary>
        [XmlElement(ElementName = "RENAVAM")]
        public string RENAVAM { get; set; }

        /// <summary>
        /// 2 - Tara em KG 
        /// </summary>
        [XmlElement(ElementName = "tara")]
        public int? Tara { get; set; }

        /// <summary>
        /// 2 - Capacidade em KG 
        /// </summary>
        [XmlElement(ElementName = "capKG")]
        public int? CapKG { get; set; }

        /// <summary>
        /// 2 - Capacidade em M3 
        /// </summary>
        [XmlElement(ElementName = "capM3")]
        public int? CapM3 { get; set; }

        /// <summary>
        /// 2 - Proprietários do Veículo. Só preenchido quando o veículo não pertencer à empresa emitente do MDF-e
        /// </summary>
        [XmlElement(ElementName = "prop")]
        public MDFeProp Prop { get; set; }

        [XmlElement(ElementName = "tpCar")]
        public MDFeTpCar TpCar { get; set; }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get
            {
                return UF.GetSiglaUfString();
            }
            set { UF = UF.SiglaParaEstado(value); }
        }

        public bool TaraSpecified { get { return Tara.HasValue; } }
        public bool CapKGSpecified { get { return CapKG.HasValue; } }
        public bool CapM3Specified { get { return CapM3.HasValue; } }
    }
}