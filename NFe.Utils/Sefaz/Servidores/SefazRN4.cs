using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRN4 : SefazSVRS4
    {
        public SefazRN4() : base()
        {
            EstadoReferente = Estado.RN;
        }

        public override Estado EstadoReferente { get; set; }
    }
}