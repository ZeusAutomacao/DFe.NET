using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.Factory;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class EventoInclusaoCondutor
    {
        public MDFeRetEventoMDFe MDFeEventoIncluirCondutor(MDFeEletronico mdfe, byte sequenciaEvento, string nome, string cpf)
        {
            var incluirCodutor = ClassesFactory.CriaEvIncCondutorMDFe(nome, cpf);

            var retorno = new ServicoController().Executar(mdfe, sequenciaEvento, incluirCodutor, MDFeTipoEvento.InclusaoDeCondutor);

            return retorno;
        }
    }
}