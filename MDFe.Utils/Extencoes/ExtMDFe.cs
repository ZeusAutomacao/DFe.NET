using System;
using DFe.Classes.Entidades;
using DFe.Utils;
using DFe.Utils.Assinatura;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Validacao;
using MDFEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFe
    {
        public static MDFEletronico Valida(this MDFEletronico mdfe)
        {
            if (mdfe == null) throw new ArgumentException("Erro de assinatura, MDFe esta null");

            var xmlMdfe = FuncoesXml.ClasseParaXmlString(mdfe);

            Validador.Valida(xmlMdfe, "MDFe_v1.00.xsd");

            var tipoModal = mdfe.InfMDFe.InfModal.Modal.GetType();
            var xmlModal = FuncoesXml.ClasseParaXmlString(mdfe.InfMDFe.InfModal);


            if (tipoModal == typeof (MDFeRodo))
            {
                Validador.Valida(xmlModal, "MDFeModalRodoviario_v1.00.xsd");
            }

            if (tipoModal == typeof (MDFeAereo))
            {
                Validador.Valida(xmlModal, "MDFeModalAereo_v1.00.xsd");
            }

            if (tipoModal == typeof (MDFeAquav))
            {
                Validador.Valida(xmlModal, "MDFeModalAquaviario_v1.00.xsd");
            }

            if (tipoModal == typeof (MDFeFerrov))
            {
                Validador.Valida(xmlModal, "MDFeModalFerroviario_v1.00.xsd");
            }

            return mdfe;
        }

        public static MDFEletronico Assina(this MDFEletronico mdfe)
        {
            if(mdfe == null) throw new ArgumentException("Erro de assinatura, MDFe esta null");

            var modeloDocumentoFiscal = (int) mdfe.InfMDFe.Ide.Mod;
            var tipoEmissao = (int) mdfe.InfMDFe.Ide.TpEmis;
            var codigoNumerico = mdfe.InfMDFe.Ide.CMDF;
            var codigoIbgeUf = (int) mdfe.InfMDFe.Ide.CUF;
            var dataEHoraEmissao = mdfe.InfMDFe.Ide.DhEmi;
            var documentoUnico = long.Parse(mdfe.InfMDFe.Emit.CNPJ);
            var numeroDocumento = mdfe.InfMDFe.Ide.NMDF;
            int serie = mdfe.InfMDFe.Ide.Serie;

            var gerarChave = new GerarChaveFiscal(modeloDocumentoFiscal, tipoEmissao, codigoNumerico,
                codigoIbgeUf, dataEHoraEmissao, documentoUnico, numeroDocumento, serie);

            mdfe.InfMDFe.Id = "MDFe" + gerarChave.Chave;
            mdfe.InfMDFe.Versao = MDFeVersaoServico.Versao100;
            mdfe.InfMDFe.Ide.CDV = gerarChave.DigitoVerificador;

            var assinatura = AssinaturaDigital.Assina(mdfe, mdfe.InfMDFe.Id, MDFeConfiguracao.X509Certificate2);

            mdfe.Signature = assinatura;

            return mdfe;
        }

        public static void SalvarXmlEmDisco(this MDFEletronico mdfe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var arquivoSalvar = MDFeConfiguracao.CaminhoSalvarXml += @"\" + mdfe.Chave() + "-mdfe.xml";

            FuncoesXml.ClasseParaArquivoXml(mdfe, arquivoSalvar);
        }

        public static string Chave(this MDFEletronico mdfe)
        {
            var chave = mdfe.InfMDFe.Id.Substring(4, 44);
            return chave;
        }

        public static string CNPJEmitente(this MDFEletronico mdfe)
        {
            var cnpj = mdfe.InfMDFe.Emit.CNPJ;

            return cnpj;
        }

        public static EstadoUF UFEmitente(this MDFEletronico mdfe)
        {
            var estadoUf = mdfe.InfMDFe.Emit.EnderEmit.UF;

            return estadoUf;
        }
    }
}
