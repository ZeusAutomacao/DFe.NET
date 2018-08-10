using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazAC : SefazSVRS
    {
        public SefazAC() : base()
        {
            EstadoReferente = Estado.AC;
        }

        public override Estado EstadoReferente { get; set; }
    }
}