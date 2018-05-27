using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazPB : SefazSVRS
    {
        public SefazPB() : base()
        {
            EstadoReferente = Estado.PB;
        }

        public override Estado EstadoReferente { get; set; }
    }
}