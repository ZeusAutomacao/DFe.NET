using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRJ : SefazSVRS
    {
        public SefazRJ() : base()
        {
            EstadoReferente = Estado.RJ;
        }

        public override Estado EstadoReferente { get; set; }
    }
}