using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazMA4 : SefazSVAN4
    {
        public SefazMA4() : base()
        {
            EstadoReferente = Estado.MA;
        }

        public override Estado EstadoReferente { get; set; }
    }
}