using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazAP4 : SefazSVRS4
    {
        public SefazAP4() : base()
        {
            EstadoReferente = Estado.AP;
        }

        public override Estado EstadoReferente { get; set; }
    }
}