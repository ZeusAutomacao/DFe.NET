using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazPI4 : SefazSVAN4
    {
        public SefazPI4() : base()
        {
            EstadoReferente = Estado.PI;
        }

        public override Estado EstadoReferente { get; set; }
    }
}