using System.Xml.Serialization;
using CTeDLL.Classes.Servicos.Tipos;

namespace CTeDLL.Classes.Servicos.Evento
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