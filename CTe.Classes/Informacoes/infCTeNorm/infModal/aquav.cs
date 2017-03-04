using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class aquav : ContainerModal
    {
        private decimal _vPrest;
        private decimal _vAfrmm;

        public decimal vPrest
        {
            get { return _vPrest.Arredondar(2); }
            set { _vPrest = value.Arredondar(2); }
        }

        public decimal vAFRMM
        {
            get { return _vAfrmm.Arredondar(2); }
            set { _vAfrmm = value.Arredondar(2); }
        }

        public string nBooking { get; set; }
        public string nCtrl { get; set; }
        public string xNavio { get; set; }

        [XmlElement(ElementName = "balsa")]
        public List<balsa> balsa { get; set; }

        public string nViag { get; set; }
        public string direc { get; set; }
        public string prtEmb { get; set; }
        public string prtTrans { get; set; }
        public string prtDest { get; set; }
        public tpNav? tpNav { get; set; }
        public bool tpNavSpecified => tpNav.HasValue;
        public string irin { get; set; }

        [XmlElement(ElementName = "detCont")]
        public List<detCont> detcont { get; set; }
    }
}