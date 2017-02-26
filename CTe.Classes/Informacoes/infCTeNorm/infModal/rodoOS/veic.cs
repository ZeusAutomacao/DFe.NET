using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class veic
    {
        public string cInt { get; set; }
        public string RENAVAM { get; set; }
        public string placa { get; set; }
        public int? tara { get; set; }
        public bool taraSpecified => tara.HasValue;
        public int? capKG { get; set; }
        public bool capKGSpecified => capKG.HasValue;
        public int? capM3 { get; set; }
        public bool capM3Specified => capM3.HasValue;
        public tpProp? tpProp { get; set; }
        public bool tpPropSpecified => tpProp.HasValue;
        public tpVeic? tpVeic { get; set; }
        public bool tpVeicSpecified => tpVeic.HasValue;
        public tpCar? tpCar { get; set; }
        public bool tpCarSpecified => tpCar.HasValue;
        public tpRod? tpRod { get; set; }
        public bool tpRodSpecified => tpRod.HasValue;

        [XmlIgnore]
        public Estado UF { get; set; }


        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }

        public prop prop { get; set; }
    }
}