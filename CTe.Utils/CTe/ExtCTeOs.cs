using CTe.Classes;
using CTe.Classes.Informacoes;
using CTe.Classes.Informacoes.infCTeNormal.infModals;
using CTe.Classes.Informacoes.Tipos;
using CTe.CTeOSDocumento.CTe.CTeOS.Servicos.Autorizacao;
using CTe.Utils.Validacao;
using DFe.Classes.Entidades;
using DFe.Utils;
using DFe.Utils.Assinatura;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using CteEletronica = CTe.CTeOSClasses.CTeOS;

namespace CTe.Utils.CTe
{
    public static class ExtCTeOs
    {
        /// <summary>
        ///     Assina um objeto CTe OS
        /// </summary>
        /// <param name="cte"></param>
        /// <param name="configuracaoServico"></param>
        /// <returns>Retorna um objeto do tipo CTe assinado</returns>
        public static void Assina(this CteEletronica cte, ConfiguracaoServico configuracaoServico = null)
        {
            if (cte == null) throw new ArgumentNullException("cte");

            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var modeloDocumentoFiscal = cte.InfCte.ide.mod;
            var tipoEmissao = (int)cte.InfCte.ide.tpEmis;
            var codigoNumerico = cte.InfCte.ide.cCT;
            var estado = cte.InfCte.ide.cUF;
            var dataEHoraEmissao = cte.InfCte.ide.dhEmi;
            var cnpj = cte.InfCte.emit.CNPJ;
            var numeroDocumento = cte.InfCte.ide.nCT;
            int serie = cte.InfCte.ide.serie;

            var dadosChave = ChaveFiscal.ObterChave(estado, dataEHoraEmissao, cnpj, modeloDocumentoFiscal, serie, numeroDocumento, tipoEmissao, codigoNumerico);

            cte.InfCte.Id = "CTe" + dadosChave.Chave;
            cte.InfCte.versao = DFe.Classes.Flags.VersaoServico.Versao400;
            cte.InfCte.ide.cDV = dadosChave.DigitoVerificador;

            var assinatura = AssinaturaDigital.Assina(cte, cte.InfCte.Id, configServico.X509Certificate2);

            cte.Signature = assinatura;
        }

        public static infCTeSupl QrCode(this CteEletronica cte, X509Certificate2 certificadoDigital,
            Encoding encoding, bool isAdicionaQrCode, string url)
        {
            if (isAdicionaQrCode == false) return null;

            if (encoding == null)
                encoding = Encoding.UTF8;

            var chave = cte.InfCte.Id.Substring(3, 44);

            var qrCode = new StringBuilder(url);
            qrCode.Append("?");
            qrCode.Append("chCTe=").Append(chave);
            qrCode.Append("&");
            qrCode.Append("tpAmb=").Append((int)cte.InfCte.ide.tpAmb);

            if (cte.InfCte.ide.tpEmis != tpEmis.teNormal
                && cte.InfCte.ide.tpEmis != tpEmis.teSVCRS
                && cte.InfCte.ide.tpEmis != tpEmis.teSVCSP
                )
            {
                var assinatura = Convert.ToBase64String(CreateSignaturePkcs1(certificadoDigital, encoding.GetBytes(chave)));
                qrCode.Append("&sign=");
                qrCode.Append(assinatura);
            }

            return new infCTeSupl
            {
                qrCodCTe = qrCode.ToString()
            };
        }

        private static byte[] CreateSignaturePkcs1(X509Certificate2 certificadoDigital, byte[] Value)
        {
            var rsa = certificadoDigital.GetRSAPrivateKey();

            RSAPKCS1SignatureFormatter rsaF = new RSAPKCS1SignatureFormatter(rsa);

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] hash = null;

            hash = sha1.ComputeHash(Value);

            rsaF.SetHashAlgorithm("SHA1");

            return rsaF.CreateSignature(hash);
        }

        public static string Chave(this CteEletronica cte)
        {
            var chave = cte.InfCte.Id.Substring(3, 44);
            return chave;
        }

        public static void SalvarXmlEmDisco(this CteEletronica cte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, cte.Chave() + "-cte.xml");

            FuncoesXml.ClasseParaArquivoXml(cte, arquivoSalvar);
        }


        /// <summary>
        ///     Converte o objeto CTe para uma string no formato XML
        /// </summary>
        /// <param name="cte"></param>
        /// <returns>Retorna uma string no formato XML com os dados da CTe</returns>
        public static string ObterXmlString(this CteEletronica cte)
        {
            return FuncoesXml.ClasseParaXmlString(cte);
        }

        public static XmlDocument CriaRequestWs(this CteEletronica cte, ConfiguracaoServico configuracaoServico = null)
        {
            var request = new XmlDocument();

            var xml = cte.ObterXmlString();

            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.cUF == Estado.PR
                || instanciaServico.cUF == Estado.MT)
                //Caso o lote seja enviado para o PR, colocar o namespace nos elementos <CTe> do lote, pois o serviço do PR o exige, conforme https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe/issues/456
                xml = xml.Replace("<CTeOS>", "<CTeOS xmlns=\"http://www.portalfiscal.inf.br/cte\">");

            request.LoadXml(xml);

            return request;
        }

        /// <summary>
        ///     Gera id, cdv, assina e faz alguns ajustes nos dados da classe CTe antes de utilizá-la
        /// </summary>
        /// <param name="cte"></param>
        /// <param name="configuracaoServico"></param>
        /// <returns>Retorna um objeto CTe devidamente tradado</returns>
        public static void ValidaSchema(this CteEletronica cte, ConfiguracaoServico configuracaoServico = null)
        {
            if (cte == null) throw new ArgumentNullException("cte");

            var xmlValidacao = cte.ObterXmlString();

            var servicoInstancia = configuracaoServico ?? ConfiguracaoServico.Instancia;
            if (!servicoInstancia.IsValidaSchemas)
                return;

            switch (cte.InfCte.versao)
            {
                case DFe.Classes.Flags.VersaoServico.Versao400:
                    Validador.Valida(xmlValidacao, "cteOS_v4.00.xsd", servicoInstancia);
                    break;
                default:
                    throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                        "a versão está inválida, somente é permitido " +
                                                        "versão 4.00");
            }

            if (cte.InfCte.ide.tpCTe != tpCTe.Complemento) // Ct-e do Tipo Anulação/Complemento não tem Informações do Modal
            {
                var xmlModal = FuncoesXml.ClasseParaXmlString(cte.InfCte.infCTeNorm.infModal);

                switch (cte.InfCte.infCTeNorm.infModal.versaoModal)
                {                    
                    case versaoModal.veM400:
                        if (cte.InfCte.infCTeNorm.infModal.ContainerModal.GetType() == typeof(aereo))
                        {
                            Validador.Valida(xmlModal, "cteModalAereo_v4.00.xsd", servicoInstancia);
                        }

                        if (cte.InfCte.infCTeNorm.infModal.ContainerModal.GetType() == typeof(aquav))
                        {
                            Validador.Valida(xmlModal, "cteModalAquaviario_v4.00.xsd", servicoInstancia);
                        }

                        if (cte.InfCte.infCTeNorm.infModal.ContainerModal.GetType() == typeof(duto))
                        {
                            Validador.Valida(xmlModal, "cteModalDutoviario_v4.00.xsd", servicoInstancia);
                        }

                        if (cte.InfCte.infCTeNorm.infModal.ContainerModal.GetType() == typeof(ferrov))
                        {
                            Validador.Valida(xmlModal, "cteModalFerroviario_v4.00.xsd", servicoInstancia);
                        }

                        if (cte.InfCte.infCTeNorm.infModal.ContainerModal.GetType() == typeof(rodo))
                        {
                            Validador.Valida(xmlModal, "cteModalRodoviario_v4.00.xsd", servicoInstancia);
                        }

                        if (cte.InfCte.infCTeNorm.infModal.ContainerModal.GetType() == typeof(multimodal))
                        {
                            Validador.Valida(xmlModal, "cteMultimodal_v4.00.xsd", servicoInstancia);
                        }

                        if (cte.InfCte.infCTeNorm.infModal.ContainerModal.GetType() == typeof(rodoOS))
                        {
                            Validador.Valida(xmlModal, "cteModalRodoviarioOS_v4.00.xsd", servicoInstancia);
                        }
                        break;
                    default:
                        throw new InvalidOperationException("Nos achamos um erro na hora de validar o schema, " +
                                                            "a versão está inválida, somente é permitido " +
                                                            "versão 4.00");
                }
            }
        }

        public static void SalvarXmlEmDisco(this retCTeOS retEnviCte, string chave, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, chave + "-cte.xml");

            FuncoesXml.ClasseParaArquivoXml(retEnviCte, arquivoSalvar);
        }
    }
}
