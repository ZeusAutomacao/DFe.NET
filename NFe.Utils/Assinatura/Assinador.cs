using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using DFe.Utils;
using DFe.Utils.Assinatura;
using Shared.DFe.Utils;
using Signature = DFe.Classes.Assinatura.Signature;

namespace NFe.Utils.Assinatura
{
    public static class Assinador
    {
        /// <summary>
        ///     Obtém a assinatura de um objeto serializável
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser assinado</typeparam>
        /// <param name="objeto">Objeto a ser assinado</param>
        /// <param name="id">Id para URI do objeto <see cref="Signature"/></param>
        /// <param name="configuracaoServico">Configuração do serviço</param>
        /// <returns>Retorna um objeto do tipo Classes.Assinatura.Signature, contendo a assinatura do objeto passado como parâmetro</returns>
        public static Signature ObterAssinatura<T>(T objeto, string id, ConfiguracaoServico configuracaoServico = null) where T : class
        {
            var cfgServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            X509Certificate2 certificadoDigital = null;
            try
            {
                certificadoDigital = CertificadoDigital.ObterCertificado(cfgServico.Certificado);
                return ObterAssinatura<T>(objeto, id, certificadoDigital, cfgServico.Certificado.ManterDadosEmCache, cfgServico.Certificado.SignatureMethodSignedXml, cfgServico.Certificado.DigestMethodReference, cfgServico.RemoverAcentos);
            }
            finally
            {
                if (!cfgServico.Certificado.ManterDadosEmCache)
                    certificadoDigital?.Reset();
            }
        }

        /// <summary>
        ///     Obtém a assinatura de um objeto serializável
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        /// <param name="id"></param>
        /// <param name="certificadoDigital">Informe o certificado digital</param>
        /// <param name="manterDadosEmCache">Validador para manter o certificado em cache</param>
        /// <param name="signatureMethod"></param>
        /// <param name="digestMethod"></param>
        /// <param name="cfgServicoRemoverAcentos"></param>
        /// <returns>Retorna um objeto do tipo Classes.Assinatura.Signature, contendo a assinatura do objeto passado como parâmetro</returns>
        public static Signature ObterAssinatura<T>(T objeto, string id, X509Certificate2 certificadoDigital,
            bool manterDadosEmCache = false, string signatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
            string digestMethod = "http://www.w3.org/2000/09/xmldsig#sha1", bool cfgServicoRemoverAcentos = false) where T : class
        {
            var objetoLocal = objeto;
            if (id == null)
                throw new Exception("Não é possível assinar um objeto evento sem sua respectiva Id!");

            try
            {
                var documento = new XmlDocument { PreserveWhitespace = true };
                var xml = cfgServicoRemoverAcentos
                    ? FuncoesXml.ClasseParaXmlString(objetoLocal).RemoverAcentos()
                    : FuncoesXml.ClasseParaXmlString(objetoLocal);

                documento.LoadXml(xml);

                var docXml = new SignedXml(documento) { SigningKey = certificadoDigital.PrivateKey };

                docXml.SignedInfo.SignatureMethod = signatureMethod;

                var reference = new Reference { Uri = "#" + id, DigestMethod = digestMethod};

                // adicionando EnvelopedSignatureTransform a referencia
                var envelopedSigntature = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(envelopedSigntature);

                var c14Transform = new XmlDsigC14NTransform();
                reference.AddTransform(c14Transform);

                docXml.AddReference(reference);

                // carrega o certificado em KeyInfoX509Data para adicionar a KeyInfo
                var keyInfo = new KeyInfo();
                keyInfo.AddClause(new KeyInfoX509Data(certificadoDigital));

                docXml.KeyInfo = keyInfo;
                docXml.ComputeSignature();

                //// recuperando a representação do XML assinado
                var xmlDigitalSignature = docXml.GetXml();
                var assinatura = FuncoesXml.XmlStringParaClasse<Signature>(xmlDigitalSignature.OuterXml);
                return assinatura;
            }
            finally
            {
                //Marcos Gerene 04/08/2018 - o objeto certificadoDigital nunca será nulo, porque se ele for nulo nem as configs para criar ele teria.

                //Se não mantém os dados do certificado em cache libera o certificado, chamando o método reset.
                //if (!manterDadosEmCache & certificadoDigital == null)
                //     certificadoDigital.Reset();
            }

        }
    }
}