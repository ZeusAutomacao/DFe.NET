using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.Factory;
using System.Collections.Generic;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class EventoInclusaoDFe
    {
        public MDFeRetEventoMDFe MDFeEventoIncluirDFe(MDFeEletronico mdfe, byte sequenciaEvento, string protocolo,
            string codigoMunicipioCarregamento, string nomeMunicipioCarregamento, List<MDFeInfDocInc> informacoesDocumentos)
        {
            var inclusao = ClassesFactory.CriaEvIncDFeMDFe(protocolo, codigoMunicipioCarregamento, nomeMunicipioCarregamento, informacoesDocumentos);

            return new ServicoController().Executar(mdfe, sequenciaEvento, inclusao, MDFeTipoEvento.InclusaoDFe);
        }
    }
}