using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazTO : SefazSVRS
    {
        public SefazTO() : base()
        {
            EstadoReferente = Estado.TO;
        }

        public override Estado EstadoReferente { get; set; }
    }
}