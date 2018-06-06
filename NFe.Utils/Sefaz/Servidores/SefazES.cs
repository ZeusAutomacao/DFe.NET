using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazES : SefazSVAN
    {
        public SefazES() : base()
        {
            EstadoReferente = Estado.ES;
        }

        public override Estado EstadoReferente { get; set; }
    }
}