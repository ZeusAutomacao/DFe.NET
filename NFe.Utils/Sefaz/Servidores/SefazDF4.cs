using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazDF4 : SefazSVAN4
    {
        public SefazDF4() : base()
        {
            EstadoReferente = Estado.DF;
        }

        public override Estado EstadoReferente { get; set; }
    }
}