using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionDadosComplementaresCTe
    {
        [XmlElement(ElementName = "Entrega")]
        public FusionEntregaCTe Entrega { get; set; }

        [XmlElement(ElementName = "xObs")]
        public string ObservacoesGerais { get; set; }

        public FusionDadosComplementaresCTe()
        {
            Entrega = new FusionEntregaCTe();
        }
    }
}