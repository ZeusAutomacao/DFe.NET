using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazPB4 : SefazSVRS4
    {
        public SefazPB4() : base()
        {
            EstadoReferente = Estado.PB;
        }

        public override Estado EstadoReferente { get; set; }
    }
}