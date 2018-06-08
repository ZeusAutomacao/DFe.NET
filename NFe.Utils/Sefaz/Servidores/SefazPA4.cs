using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazPA4 : SefazSVAN4
    {
        public SefazPA4() : base()
        {
            EstadoReferente = Estado.PA;
        }

        public override Estado EstadoReferente { get; set; }
    }
}