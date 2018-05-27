using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazAC4 : SefazSVRS4
    {
        public SefazAC4() : base()
        {
            EstadoReferente = Estado.AC;
        }

        public override Estado EstadoReferente { get; set; }
    }
}