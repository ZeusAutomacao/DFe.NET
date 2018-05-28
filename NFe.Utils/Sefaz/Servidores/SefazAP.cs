using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazAP : SefazSVRS
    {
        public SefazAP() : base()
        {
            EstadoReferente = Estado.AP;
        }

        public override Estado EstadoReferente { get; set; }
    }
}