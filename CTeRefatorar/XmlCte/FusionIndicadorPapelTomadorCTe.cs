using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionIndicadorPapelTomadorCTe
    {
        [XmlElement(ElementName = "toma")]
        public FusionTipoTomadorCTe TipoTomador { get; set; }
    }
}