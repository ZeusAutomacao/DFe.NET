using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRS4 : SefazSVRS4
    {
        public SefazRS4() : base()
        {
            EstadoReferente = Estado.RS;
        }

        public override Estado EstadoReferente { get; set; }
    }
}