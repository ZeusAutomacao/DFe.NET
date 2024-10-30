using System;
using System.Text;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Classes.Servicos.Tipos;
using CTe.Utils.CTe;
using CTeEletronico = CTe.Classes.CTe;

namespace CTe.Servicos.Eventos
{
    public class FactoryEvento
    {
        //Vou manter para evitar quebra de código pois a classe e o metodo são publicos
        public static eventoCTe CriaEvento(CTeEletronico cte, CTeTipoEvento cTeTipoEvento, int sequenciaEvento, EventoContainer container, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            return CriaEvento(cTeTipoEvento, sequenciaEvento, cte.Chave(), cte.infCte.emit.CNPJ, container, configServico);
        }

        public static eventoCTe CriaEvento(CTeTipoEvento cTeTipoEvento, int sequenciaEvento, string chave, string cnpj, EventoContainer container, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var id = new StringBuilder("ID");
            id.Append((int)cTeTipoEvento);
            id.Append(chave);

            if (configServico.VersaoLayout == versao.ve200 || configServico.VersaoLayout == versao.ve300)
                id.Append(sequenciaEvento.ToString("D2"));

            if (configServico.VersaoLayout == versao.ve400)
                id.Append(sequenciaEvento.ToString("D3"));

            return new eventoCTe
            {
                versao = configServico.VersaoLayout,
                infEvento = new infEventoEnv(configServico)
                {
                    tpAmb = configServico.tpAmb,
                    CNPJ = cnpj,
                    cOrgao = configServico.cUF,
                    chCTe = chave,
                    dhEvento = DateTimeOffset.Now,
                    nSeqEvento = sequenciaEvento,
                    tpEvento = cTeTipoEvento,
                    detEvento = new detEvento
                    {
                        versaoEvento = configServico.VersaoLayout,
                        EventoContainer = container
                    },
                    Id = id.ToString()
                }
            };
        }
    }
}