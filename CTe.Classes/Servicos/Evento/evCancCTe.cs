using System.Xml.Serialization;

namespace CTe.Classes.Servicos.Evento
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class evCancCTe : EventoContainer
    {
        public evCancCTe()
        {
            descEvento = "Cancelamento";
        }

        public string descEvento { get; set; }

        public string nProt { get; set; }

        public string xJust { get; set; }
    }
}