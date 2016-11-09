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
using System.Security.Cryptography.Xml;
using System.Xml;
using Signature = NFe.Classes.Assinatura.Signature;

namespace NFe.Utils.Assinatura
{
    public static class Assinador
    {
        /// <summary>
        ///     Obtém a assinatura de um objeto serializável
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        /// <param name="id"></param>
        /// <returns>Retorna um objeto do tipo Classes.Assinatura.Signature, contendo a assinatura do objeto passado como parâmetro</returns>
        public static Signature ObterAssinatura<T>(T objeto, string id) where T : class
        {
            var objetoLocal = objeto;
            if (id == null)
                throw new Exception("Não é possível assinar um objeto evento sem sua respectiva Id!");

            var certificado = string.IsNullOrEmpty(ConfiguracaoServico.Instancia.Certificado.Arquivo)
                ? CertificadoDigital.ObterDoRepositorio(ConfiguracaoServico.Instancia.Certificado.Serial, ConfiguracaoServico.Instancia.Certificado.Senha)
                : CertificadoDigital.ObterDeArquivo(ConfiguracaoServico.Instancia.Certificado.Arquivo, ConfiguracaoServico.Instancia.Certificado.Senha);

            var documento = new XmlDocument {PreserveWhitespace = true};
            documento.LoadXml(FuncoesXml.ClasseParaXmlString(objetoLocal));
            var docXml = new SignedXml(documento) {SigningKey = certificado.PrivateKey};
            var reference = new Reference {Uri = "#" + id};

            // adicionando EnvelopedSignatureTransform a referencia
            var envelopedSigntature = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(envelopedSigntature);

            var c14Transform = new XmlDsigC14NTransform();
            reference.AddTransform(c14Transform);

            docXml.AddReference(reference);

            // carrega o certificado em KeyInfoX509Data para adicionar a KeyInfo
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificado));

            docXml.KeyInfo = keyInfo;
            docXml.ComputeSignature();

            //// recuperando a representação do XML assinado
            var xmlDigitalSignature = docXml.GetXml();
            var assinatura = FuncoesXml.XmlStringParaClasse<Signature>(xmlDigitalSignature.OuterXml);
            return assinatura;
        }
    }
}