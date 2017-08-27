using DFe.DocumentosEletronicos.Ext;
using DFe.DocumentosEletronicos.NFe.Classes.Servicos.Status;
using DFe.DocumentosEletronicos.NFe.Configuracao;

namespace DFe.DocumentosEletronicos.NFe.Servicos.Factory
{
    public static class ClassesFactory
    {
        public static consStatServ CriaConsStatServ(NFeBaseConfig config)
        {
            var consStatServ = new consStatServ
            {
                versao = config.VersaoNfeStatusServico.GetVersaoString(),
                tpAmb = config.TipoAmbiente,
                cUF = config.EstadoUf
            };

            return consStatServ;
        }
    }
}