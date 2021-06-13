/********************************************************************************/
/* Projeto: Biblioteca ZeusMDFe                                                 */
/* Biblioteca C# para emissão de Manifesto Eletrônico Fiscal de Documentos      */
/* (https://mdfe-portal.sefaz.rs.gov.br/                                        */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DFe.Classes.Entidades;
using DFe.Utils;
using DFe.Utils.Assinatura;
using MDFe.Classes.Flags;
using MDFe.Classes.Informacoes;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;
using MDFEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFe
    {
        public static MDFEletronico Valida(this MDFEletronico mdfe)
        {
            if (mdfe == null) throw new ArgumentException("Erro de assinatura, MDFe esta null");

            var xmlMdfe = FuncoesXml.ClasseParaXmlString(mdfe);

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlMdfe, "MDFe_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlMdfe, "MDFe_v3.00.xsd");
                    break;
            }

            var tipoModal = mdfe.InfMDFe.InfModal.Modal.GetType();
            var xmlModal = FuncoesXml.ClasseParaXmlString(mdfe.InfMDFe.InfModal);


            if (tipoModal == typeof (MDFeRodo))
            {
                switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        Validador.Valida(xmlModal, "MDFeModalRodoviario_v1.00.xsd");
                        break;
                    case VersaoServico.Versao300:
                        Validador.Valida(xmlModal, "MDFeModalRodoviario_v3.00.xsd");
                        break;
                }
            }

            if (tipoModal == typeof (MDFeAereo))
            {
                switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        Validador.Valida(xmlModal, "MDFeModalAereo_v1.00.xsd");
                        break;
                    case VersaoServico.Versao300:
                        Validador.Valida(xmlModal, "MDFeModalAereo_v3.00.xsd");
                        break;
                }
            }

            if (tipoModal == typeof (MDFeAquav))
            {
                switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        Validador.Valida(xmlModal, "MDFeModalAquaviario_v1.00.xsd");
                        break;
                    case VersaoServico.Versao300:
                        Validador.Valida(xmlModal, "MDFeModalAquaviario_v3.00.xsd");
                        break;
                }
            }

            if (tipoModal == typeof (MDFeFerrov))
            {
                switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
                {
                    case VersaoServico.Versao100:
                        Validador.Valida(xmlModal, "MDFeModalFerroviario_v1.00.xsd");
                        break;
                    case VersaoServico.Versao300:
                        Validador.Valida(xmlModal, "MDFeModalFerroviario_v3.00.xsd");
                        break;
                }
            }

            return mdfe;
        }

        public static MDFEletronico Assina(this MDFEletronico mdfe, EventHandler<string> eventHandlerChaveMdfe = null, object quemInvocouEventoChaveMDFe = null)
        {
            if(mdfe == null) throw new ArgumentException("Erro de assinatura, MDFe esta null");

            var modeloDocumentoFiscal = mdfe.InfMDFe.Ide.Mod;
            var tipoEmissao = (int) mdfe.InfMDFe.Ide.TpEmis;
            var codigoNumerico = mdfe.InfMDFe.Ide.CMDF;
            var estado = mdfe.InfMDFe.Ide.CUF;
            var dataEHoraEmissao = mdfe.InfMDFe.Ide.DhEmi;
            var cnpj = mdfe.InfMDFe.Emit.CNPJ;
            var numeroDocumento = mdfe.InfMDFe.Ide.NMDF;
            int serie = mdfe.InfMDFe.Ide.Serie;


            if (cnpj == null)
            {
                cnpj = mdfe.InfMDFe.Emit.CPF.PadLeft(14, '0');
            }

            var dadosChave = ChaveFiscal.ObterChave(estado, dataEHoraEmissao, cnpj, modeloDocumentoFiscal, serie, numeroDocumento, tipoEmissao, codigoNumerico);

            mdfe.InfMDFe.Id = "MDFe" + dadosChave.Chave;

            if (eventHandlerChaveMdfe != null)
                eventHandlerChaveMdfe.Invoke(quemInvocouEventoChaveMDFe, dadosChave.Chave);

            mdfe.InfMDFe.Versao = MDFeConfiguracao.VersaoWebService.VersaoLayout;
            mdfe.InfMDFe.Ide.CDV = dadosChave.DigitoVerificador;

            var assinatura = AssinaturaDigital.Assina(mdfe, mdfe.InfMDFe.Id, MDFeConfiguracao.X509Certificate2);

            mdfe.Signature = assinatura;

            return mdfe;
        }

        public static string XmlString(this MDFEletronico mdfe)
        {
            return FuncoesXml.ClasseParaXmlString(mdfe);
        }

        public static void SalvarXmlEmDisco(this MDFEletronico mdfe, string nomeArquivo = null)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            if (string.IsNullOrEmpty(nomeArquivo))
                nomeArquivo = Path.Combine(MDFeConfiguracao.CaminhoSalvarXml, mdfe.Chave() + "-mdfe.xml");

            FuncoesXml.ClasseParaArquivoXml(mdfe, nomeArquivo);
        }

        public static string Chave(this MDFEletronico mdfe)
        {
            var chave = mdfe.InfMDFe.Id.Substring(4, 44);
            return chave;
        }

        public static int AmbienteSefazInt(this MDFEletronico mdfe)
        {
            var ambiente = mdfe.InfMDFe.Ide.TpAmb;

            return (int) ambiente;
        }

        public static string CNPJEmitente(this MDFEletronico mdfe)
        {
            var cnpj = mdfe.InfMDFe.Emit.CNPJ;

            return cnpj;
        }

        public static string CPFEmitente(this MDFEletronico mdfe)
        {
            var cpf = mdfe.InfMDFe.Emit.CPF;

            return cpf;
        }

        public static string CNPJouCPFEmitente(this MDFEletronico mdfe)
        {
            var cnpj = CNPJEmitente(mdfe);

            if (cnpj != null) return cnpj;

            return CPFEmitente(mdfe).PadLeft(14, '0');
        }

        public static Estado UFEmitente(this MDFEletronico mdfe)
        {
            var estadoUf = mdfe.InfMDFe.Emit.EnderEmit.UF;

            return estadoUf;
        }

        public static long CodigoIbgeMunicipioEmitente(this MDFEletronico mdfe)
        {
            var codigo = mdfe.InfMDFe.Emit.EnderEmit.CMun;

            return codigo;
        }

        public static infMDFeSupl QrCode(this MDFEletronico mdfe, X509Certificate2 certificadoDigital,
            Encoding encoding = null)
        {
            if (encoding == null) 
                encoding = Encoding.UTF8;

            var qrCode = new StringBuilder(@"https://dfe-portal.svrs.rs.gov.br/mdfe/qrCode");
            qrCode.Append("?");
            qrCode.Append("chMDFe=").Append(mdfe.Chave());
            qrCode.Append("&");
            qrCode.Append("tpAmb=").Append(mdfe.AmbienteSefazInt());

            switch (mdfe.InfMDFe.Ide.TpEmis)
            {
                case MDFeTipoEmissao.Contingencia:
                    var assinatura = Convert.ToBase64String(CreateSignaturePkcs1(certificadoDigital, encoding.GetBytes(mdfe.Chave())));
                    qrCode.Append("&sign=");
                    qrCode.Append(assinatura);
                    break;
            }

            return new infMDFeSupl
            {
                qrCodMDFe = qrCode.ToString()
            };
        }

        private static byte[] CreateSignaturePkcs1(X509Certificate2 certificado, byte[] Value)

        {
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificado.PrivateKey;

            RSAPKCS1SignatureFormatter rsaF = new RSAPKCS1SignatureFormatter(rsa);

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] hash = null;

            hash = sha1.ComputeHash(Value);

            rsaF.SetHashAlgorithm("SHA1");

            return rsaF.CreateSignature(hash);

        }
    }
}
