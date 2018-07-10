using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazPI : SefazSVAN
    {
        public SefazPI() : base()
        {
            EstadoReferente = Estado.PI;
        }

        public override Estado EstadoReferente { get; set; }
    }
}