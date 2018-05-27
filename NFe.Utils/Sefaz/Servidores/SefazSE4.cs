using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazSE4 : SefazSVRS4
    {
        public SefazSE4() : base()
        {
            EstadoReferente = Estado.SE;
        }

        public override Estado EstadoReferente { get; set; }
    }
}