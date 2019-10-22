/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
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

                documento.LoadXml(cfgServicoRemoverAcentos
                    ? FuncoesXml.ClasseParaXmlString(objetoLocal).RemoverAcentos()
                    : FuncoesXml.ClasseParaXmlString(objetoLocal));

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
