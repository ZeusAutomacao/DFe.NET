using DFe.MDFe.Classes.Retorno.MDFeRecepcao;
using DFe.MDFe.Servicos.RecepcaoMDFe;
using MdfeEletronico = DFe.MDFe.Classes.Informacoes.MDFe;

namespace DFe.MDFe.Facade
{
    public class MDFeFacade
    {
        private readonly MDFeEnviarLote _enviarLote;

        public MDFeFacade()
        {
            _enviarLote = new MDFeEnviarLote();
        }

        public MDFeRetEnviMDFe EnviarLote(long lote, MdfeEletronico mdfe)
        {
            return _enviarLote.EnviarLote(lote, mdfe);
        }
    }
}