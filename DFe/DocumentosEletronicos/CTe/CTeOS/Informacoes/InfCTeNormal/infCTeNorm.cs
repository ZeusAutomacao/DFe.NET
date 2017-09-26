using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.AutorizadoDownloadXml;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.infCteAnu;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.infCteComp;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.InfCTeNormal
{
    public class infCTeNorm
    {
        public infServico infServico { get; set; }

        public infDocRef infDocRef { get; set; }

        public seg seg { get; set; }

        public infModal infModal { get; set; }

        public infCteSub infCteSub { get; set; }

        public infCteComp infCteComp { get; set; }

        public infCteAnu infCteAnu { get; set; }

        public autXML autXml { get; set; }
    }
}