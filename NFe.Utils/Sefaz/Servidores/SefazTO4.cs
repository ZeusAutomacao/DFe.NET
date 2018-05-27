using DFe.Classes.Entidades;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazTO4 : SefazSVRS4
    {
        public SefazTO4() : base()
        {
            EstadoReferente = Estado.TO;
        }

        public override Estado EstadoReferente { get; set; }
    }
}