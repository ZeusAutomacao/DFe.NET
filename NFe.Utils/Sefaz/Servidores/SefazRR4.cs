using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRR4 : SefazSVRS4
    {
        public SefazRR4() : base()
        {
            EstadoReferente = Estado.RR;
        }

        public override Estado EstadoReferente { get; set; }
    }
}