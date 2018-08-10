using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazES4 : SefazSVAN4
    {
        public SefazES4() : base()
        {
            EstadoReferente = Estado.ES;
        }

        public override Estado EstadoReferente { get; set; }
    }
}