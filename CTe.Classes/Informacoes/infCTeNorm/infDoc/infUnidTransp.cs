using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infUnidTransp
    {
        private decimal? _qtdRat;
        public tpUnidTransp tpUnidTransp { get; set; }

        public string idUnidTransp { get; set; }

        [XmlElement("lacUnidTransp")]
        public List<lacUnidTransp> lacUnidTransp { get; set; }

        [XmlElement("infUnidCarga")]
        public List<infUnidCarga> infUnidCarga { get; set; }

        public decimal? qtdRat
        {
            get { return _qtdRat.Arredondar(3); }
            set { _qtdRat = value.Arredondar(3); }
        }

        public bool qtdRatSpecified => qtdRat.HasValue;
    }
}
