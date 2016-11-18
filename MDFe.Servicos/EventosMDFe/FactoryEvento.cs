using System;
using DFe.Utils.Assinatura;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFeEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public static class FactoryEvento
    {
        public static MDFeEventoMDFe CriaEvento(MDFeEletronico MDFe, MDFeTipoEvento tipoEvento, byte sequenciaEvento, MDFeEventoContainer evento)
        {
            var eventoMDFe = new MDFeEventoMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeRecepcaoEvento,
                InfEvento = new MDFeInfEvento
                {
                    Id = "ID" + (long)tipoEvento + MDFe.Chave() + sequenciaEvento.ToString("D2"),
                    TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                    CNPJ = MDFe.CNPJEmitente(),
                    COrgao = MDFe.UFEmitente(),
                    ChMDFe = MDFe.Chave(),
                    DetEvento = new MDFeDetEvento
                    {
                        VersaoServico = VersaoServico.Versao100,
                        EventoContainer = evento
                    },
                    DhEvento = DateTime.Now,
                    NSeqEvento = sequenciaEvento,
                    TpEvento = tipoEvento
                }
            };

            eventoMDFe.Signature = AssinaturaDigital.Assina(eventoMDFe, eventoMDFe.InfEvento.Id,
                MDFeConfiguracao.X509Certificate2);


            return eventoMDFe;
        }
    }
}