using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes;

namespace CTeDLL.Classes.Informacoes.Valores
{
    public class vPrest
    {
        public decimal vTPrest
        {
            get { return _vTPrest.Arredondar(2); }
            set { _vTPrest = value.Arredondar(2); }
        }

        public decimal vRec
        {
            get { return _vRec.Arredondar(2); }
            set { _vRec = value.Arredondar(2); }
        }

        [XmlElement("Comp")]
        public List<Comp> Comp;

        private decimal _vTPrest;
        private decimal _vRec;
    }
}
