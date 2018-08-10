using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazDF : SefazSVAN
    {
        public SefazDF() : base()
        {
            EstadoReferente = Estado.DF;
        }

        public override Estado EstadoReferente { get; set; }
    }
}