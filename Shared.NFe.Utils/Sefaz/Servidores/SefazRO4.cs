using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRO4 : SefazSVRS4
    {
        public SefazRO4() : base()
        {
            EstadoReferente = Estado.RO;
        }

        public override Estado EstadoReferente { get; set; }
    }
}