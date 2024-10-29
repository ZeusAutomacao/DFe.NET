using System.Xml.Serialization;

namespace CTe.Classes.Servicos.Evento
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class evPrestDesacordo : EventoContainer
    {
        public evPrestDesacordo()
        {
            descEvento = "Prestação do Serviço em Desacordo";
        }

        public string descEvento { get; set; }

        public string indDesacordoOper { get; set; }

        public string xObs { get; set; }
    }
}