using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRJ4 : SefazSVRS4
    {
        public SefazRJ4() : base()
        {
            EstadoReferente = Estado.RJ;
        }

        public override Estado EstadoReferente { get; set; }
    }
}