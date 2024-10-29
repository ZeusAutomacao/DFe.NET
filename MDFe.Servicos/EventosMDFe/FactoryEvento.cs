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
        public static MDFeEventoMDFe CriaEvento(MDFeEletronico MDFe, MDFeTipoEvento tipoEvento, byte sequenciaEvento, MDFeEventoContainer evento)
        {
            var eventoMDFe = new MDFeEventoMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoLayout,
                InfEvento = new MDFeInfEvento
                {
                    Id = "ID" + (long)tipoEvento + MDFe.Chave() + sequenciaEvento.ToString("D2"),
                    TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                    COrgao = MDFe.UFEmitente(),
                    ChMDFe = MDFe.Chave(),
                    DetEvento = new MDFeDetEvento
                    {
                        VersaoServico = MDFeConfiguracao.VersaoWebService.VersaoLayout,
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

            eventoMDFe.Assinar();

            return eventoMDFe;
        }
    }
}