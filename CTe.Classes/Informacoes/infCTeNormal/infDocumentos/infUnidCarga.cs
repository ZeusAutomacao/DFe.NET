using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal.infDocumentos
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

        public bool qtdRatSpecified { get { return qtdRat.HasValue; } }
    }
}