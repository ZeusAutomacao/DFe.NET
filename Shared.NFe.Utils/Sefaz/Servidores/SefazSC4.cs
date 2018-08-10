using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazSC4 : SefazSVRS4
    {
        public SefazSC4() : base()
        {
            EstadoReferente = Estado.SC;
        }

        public override Estado EstadoReferente { get; set; }
    }
}