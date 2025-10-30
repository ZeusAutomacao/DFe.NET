using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Complemento;
using DFe.Classes;

namespace CTe.CTeOSDocumento.CTe.Classes.Informacoes.Valores
{
    public class vPrestOs
    {
        [XmlElement("vTPrest", Order = 0)]
        public decimal vTPrest
        {
            get { return _vTPrest.Arredondar(2); }
            set { _vTPrest = value.Arredondar(2); }
        }

        [XmlElement("vRec", Order = 1)]
        public decimal vRec
        {
            get { return _vRec.Arredondar(2); }
            set { _vRec = value.Arredondar(2); }
        }

        [XmlElement("Comp", Order = 2)]
        public List<Comp> Comp { get; set; }

        private decimal _vTPrest;
        private decimal _vRec;
    }
}