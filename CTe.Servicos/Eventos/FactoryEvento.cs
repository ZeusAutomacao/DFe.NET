using System;
using System.Text;
using CTe.Utils.Extencoes;
using CTeDLL.Classes.Servicos.Evento;
using CTeDLL.Classes.Servicos.Evento.Flags;
using CTeEletronico = CTe.Classes.CTe;

namespace CTeDLL.Servicos.Eventos
{
    public class FactoryEvento
    {
        public static eventoCTe CriaEvento(CTeEletronico cte, TipoEvento tipoEvento, int sequenciaEvento, EventoContainer container)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            var id = new StringBuilder("ID");
            id.Append((int)tipoEvento);
            id.Append(cte.Chave());
            id.Append(sequenciaEvento.ToString("D2"));

            return new eventoCTe
            {
                versao = configuracaoServico.VersaoLayout,
                infEvento = new infEventoEnv
                {
                    tpAmb = configuracaoServico.tpAmb,
                    CNPJ = cte.infCte.emit.CNPJ,
                    cOrgao = configuracaoServico.cUF,
                    chCTe = cte.Chave(),
                    dhEvento = DateTime.Now,
                    nSeqEvento = sequenciaEvento,
                    tpEvento = tipoEvento,
                    detEvento = new detEvento
                    {
                        versaoEvento = configuracaoServico.VersaoLayout,
                        EventoContainer = container
                    },
                    Id = id.ToString()
                }
            };
        }
    }
}