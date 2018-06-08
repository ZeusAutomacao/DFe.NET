using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRR : SefazSVRS
    {
        public SefazRR() : base()
        {
            EstadoReferente = Estado.RR;
        }

        public override Estado EstadoReferente { get; set; }
    }
}