using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS
{
    public class veic
    {
        public string cInt { get; set; }
        public string RENAVAM { get; set; }
        public string placa { get; set; }
        public int? tara { get; set; }
        public bool taraSpecified { get { return tara.HasValue; } }
        public int? capKG { get; set; }
        public bool capKGSpecified { get { return capKG.HasValue; } }
        public int? capM3 { get; set; }
        public bool capM3Specified { get { return capM3.HasValue; } }
        public tpProp? tpProp { get; set; }
        public bool tpPropSpecified { get { return tpProp.HasValue; } }
        public tpVeic? tpVeic { get; set; }
        public bool tpVeicSpecified { get { return tpVeic.HasValue; } }
        public tpRod? tpRod { get; set; }
        public bool tpRodSpecified { get { return tpRod.HasValue; } }
        public tpCar? tpCar { get; set; }
        public bool tpCarSpecified { get { return tpCar.HasValue; } }

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