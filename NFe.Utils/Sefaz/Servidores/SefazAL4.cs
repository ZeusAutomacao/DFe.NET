using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazAL4 : SefazSVRS4
    {
        public SefazAL4() : base()
        {
            EstadoReferente = Estado.AL;
        }

        public override Estado EstadoReferente { get; set; }
    }
}