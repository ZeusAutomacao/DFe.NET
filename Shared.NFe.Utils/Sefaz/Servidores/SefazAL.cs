using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazAL : SefazSVRS
    {
        public SefazAL() : base()
        {
            EstadoReferente = Estado.AL;
        }

        public override Estado EstadoReferente { get; set; }
    }
}