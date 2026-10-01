using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using System.Collections.Generic;

namespace MDFe.Servicos.EventosMDFe
{
    /// <summary>
    ///     Dados para emitir o evento de inclusão de DF-e no MDFe.
    /// </summary>
    public class MDFeComandoInclusaoDFe : MDFeComandoEvento
    {
        public string Protocolo { get; set; }
        public string CodigoMunicipioCarregamento { get; set; }
        public string NomeMunicipioCarregamento { get; set; }
        public List<MDFeInfDocInc> InformacoesDocumentos { get; set; }
    }
}