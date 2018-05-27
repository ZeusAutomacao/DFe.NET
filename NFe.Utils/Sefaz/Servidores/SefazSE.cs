using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazSE : SefazSVRS
    {
        public SefazSE() : base()
        {
            EstadoReferente = Estado.SE;
        }

        public override Estado EstadoReferente { get; set; }
    }
}