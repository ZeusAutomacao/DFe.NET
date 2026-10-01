using System.Xml.Serialization;

namespace CTe.Classes.Servicos.Evento
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class evCancPrestDesacordo : EventoContainer
    {
        public evCancPrestDesacordo()
        {
            this.descEvento = "Cancelamento Prestação do Serviço em Desacordo";
        }

        public string descEvento { get; set; }

        public string nProtEvPrestDes { get; set; }

    }
}
