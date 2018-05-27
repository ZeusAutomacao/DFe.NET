using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazRO : SefazSVRS
    {
        public SefazRO() : base()
        {
            EstadoReferente = Estado.RO;
        }

        public override Estado EstadoReferente { get; set; }
    }
}