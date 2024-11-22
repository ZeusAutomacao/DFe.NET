using System;
using MDFe.Classes.Extencoes;
using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Utils.Configuracoes;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public static class FactoryEvento
    {
        public static MDFeEventoMDFe CriaEvento(MDFeEletronico MDFe, MDFeTipoEvento tipoEvento, byte sequenciaEvento, MDFeEventoContainer evento, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoMDFe = new MDFeEventoMDFe
            {
                Versao = config.VersaoWebService.VersaoLayout,
                InfEvento = new MDFeInfEvento
                {
                    Id = "ID" + (long)tipoEvento + MDFe.Chave() + sequenciaEvento.ToString("D2"),
                    TpAmb = config.VersaoWebService.TipoAmbiente,
                    COrgao = MDFe.UFEmitente(),
                    ChMDFe = MDFe.Chave(),
                    DetEvento = new MDFeDetEvento
                    {
                        VersaoServico = config.VersaoWebService.VersaoLayout,
                        EventoContainer = evento
                    },
                    DhEvento = DateTime.Now,
                    NSeqEvento = sequenciaEvento,
                    TpEvento = tipoEvento
                }
            };

            eventoMDFe.InfEvento.CNPJ = MDFe.CNPJEmitente();

            var cpfEmitente = MDFe.CPFEmitente();
            if (cpfEmitente != null)
            {
                eventoMDFe.InfEvento.CPF = cpfEmitente;
            }

            eventoMDFe.Assinar(config);

            return eventoMDFe;
        }
    }
}