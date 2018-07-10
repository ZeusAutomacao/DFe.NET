using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRS : SefazSVRS
    {
        public SefazRS() : base()
        {
            EstadoReferente = Estado.RS;
        }

        public override Estado EstadoReferente { get; set; }
    }
}