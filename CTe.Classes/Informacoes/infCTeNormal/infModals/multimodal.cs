using CTe.Classes.Informacoes.Tipos;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
{
    public class multimodal : ContainerModal
    {
        public string COTM { get; set; }
        public indNegociavel indNegociavel { get; set; }

        public segMultiModal seg { get; set; }
    }

    public class segMultiModal
    {
        public infSeg infSeg { get; set; }
        public string nApol { get; set; }
        public string nAver { get; set; }
    }

    public class infSeg
    {
        public string xSeg { get; set; }
        public string CNPJ { get; set; }
    }
}