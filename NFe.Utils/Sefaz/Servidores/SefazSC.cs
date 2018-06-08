using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazSC : SefazSVRS
    {
        public SefazSC() : base()
        {
            EstadoReferente = Estado.SC;
        }

        public override Estado EstadoReferente { get; set; }
    }
}