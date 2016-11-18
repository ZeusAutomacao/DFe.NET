using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeEvento;
using MDFeEletronica = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class ServicoMDFeEvento
    {
        public MDFeRetEventoMDFe MDFeEventoIncluirCondutor(
            MDFeEletronica mdfe, byte sequenciaEvento, string nome,
            string cpf)
        {
            var eventoIncluirCondutor = new EventoInclusaoCondutor();

            return eventoIncluirCondutor.MDFeEventoIncluirCondutor(mdfe, sequenciaEvento, nome, cpf);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramentoMDFeEventoEncerramento(MDFeEletronica mdfe, byte sequenciaEvento, string protocolo)
        {
            var eventoEncerramento = new EventoEncerramento();

            return eventoEncerramento.MDFeEventoEncerramento(mdfe, sequenciaEvento, protocolo);
        }
    }
}