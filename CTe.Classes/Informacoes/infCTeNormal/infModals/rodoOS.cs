using CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
{
    public class rodoOS : ContainerModal
    {
        public string TAF { get; set; }

        public string NroRegEstadual { get; set; }

        public veic veic { get; set; }

        public infFretamento infFretamento { get; set; }
    }
}