using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infUnidCarga
    {
        private decimal? _qtdRat;
        public tpUnidCarga tpUnidCarga { get; set; }

        public string idUnidCarga { get; set; }

        [XmlElement("lacUnidCarga")]
        public List<lacUnidCarga> lacUnidCarga { get; set; }

        public decimal? qtdRat
        {
            get { return _qtdRat.Arredondar(3); }
            set { _qtdRat = value.Arredondar(3); }
        }

        public bool qtdRatSpecified => qtdRat.HasValue;
    }
}
