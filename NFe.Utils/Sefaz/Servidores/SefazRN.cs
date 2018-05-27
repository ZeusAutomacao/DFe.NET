using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRN : SefazSVRS
    {
        public SefazRN() : base()
        {
            EstadoReferente = Estado.RN;
        }

        public override Estado EstadoReferente { get; set; }
    }
}