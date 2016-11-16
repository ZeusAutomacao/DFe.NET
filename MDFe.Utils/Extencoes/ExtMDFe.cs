using System;
using DFe.Utils;
using DFe.Utils.Assinatura;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;
using MDFe.Utils.Configuracoes;
using MDFEletronico = ManifestoDocumentoFiscalEletronico.Classes.Informacoes.MDFe;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFe
    {
        public static MDFEletronico Valida(this MDFEletronico mdfe)
        {
            if (mdfe == null) throw new ArgumentException("Erro de assinatura, MDFe esta null");




            return null;

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

            var assinatura = AssinaturaDigital.Assina(mdfe, mdfe.InfMDFe.Id, Configuracao.X509Certificate2);

            mdfe.Signature = assinatura;

            return mdfe;
        }
    }
}
