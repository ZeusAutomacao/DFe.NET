using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazPA : SefazSVAN
    {
        public SefazPA() : base()
        {
            EstadoReferente = Estado.PA;
        }

        public override Estado EstadoReferente { get; set; }
    }
}