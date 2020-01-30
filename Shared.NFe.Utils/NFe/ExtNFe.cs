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
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using DFe.Classes.Assinatura;
using DFe.Utils;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Assinatura;
using NFe.Utils.Validacao;

namespace NFe.Utils.NFe
{
    public static class ExtNFe
    {
        /// <summary>
        ///     Carrega um arquivo XML para um objeto da classe NFe
        /// </summary>
        /// <param name="nfe"></param>
        /// <param name="arquivoXml">arquivo XML</param>
        /// <returns>Retorna uma NFe carregada com os dados do XML</returns>
        public static Classes.NFe CarregarDeArquivoXml(this Classes.NFe nfe, string arquivoXml)
        {
            var s = FuncoesXml.ObterNodeDeArquivoXml(typeof (Classes.NFe).Name, arquivoXml);
            return FuncoesXml.XmlStringParaClasse<Classes.NFe>(s);
        }

        /// <summary>
        ///     Converte o objeto NFe para uma string no formato XML
        /// </summary>
        /// <param name="nfe"></param>
        /// <returns>Retorna uma string no formato XML com os dados da NFe</returns>
        public static string ObterXmlString(this Classes.NFe nfe)
        {
            return FuncoesXml.ClasseParaXmlString(nfe);
        }

        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto NFe
        /// </summary>
        /// <param name="nfe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo NFe</returns>
        public static Classes.NFe CarregarDeXmlString(this Classes.NFe nfe, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof (Classes.NFe).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<Classes.NFe>(s);
        }

        /// <summary>
        ///     Grava os dados do objeto NFe em um arquivo XML
        /// </summary>
        /// <param name="nfe">Objeto NFe</param>
        /// <param name="arquivoXml">Diretório com nome do arquivo a ser gravado</param>
        public static void SalvarArquivoXml(this Classes.NFe nfe, string arquivoXml)
        {
            FuncoesXml.ClasseParaArquivoXml(nfe, arquivoXml);
        }

        /// <summary>
        ///     Gera id, cdv, assina e faz alguns ajustes nos dados da classe NFe antes de utilizá-la
        /// </summary>
        /// <param name="nfe"></param>
        /// <param name="cfgServico1"></param>
        /// <returns>Retorna um objeto NFe devidamente tradado</returns>
        public static Classes.NFe Valida(this Classes.NFe nfe, ConfiguracaoServico cfgServico = null)
        {
            if (nfe == null) throw new ArgumentNullException("nfe");

            var versao = (Decimal.Parse(nfe.infNFe.versao, CultureInfo.InvariantCulture));

            var xmlNfe = nfe.ObterXmlString();
            var config = cfgServico ?? ConfiguracaoServico.Instancia;
            if (versao < 3)
                Validador.Valida(ServicoNFe.NfeRecepcao, config.VersaoNfeRecepcao, xmlNfe, false, config);
            if (versao >= 3)
                Validador.Valida(ServicoNFe.NFeAutorizacao, config.VersaoNFeAutorizacao, xmlNfe, false, config);

            return nfe; //Para uso no formato fluent
        }

        /// <summary>
        ///     Assina um objeto NFe
        /// </summary>
        /// <param name="nfe"></param>
        /// <param name="cfgServico">ConfiguracaoServico para uso na classe Assinador</param>
        /// <returns>Retorna um objeto do tipo NFe assinado</returns>
        public static Classes.NFe Assina(this Classes.NFe nfe, ConfiguracaoServico cfgServico = null, X509Certificate2 _certificado = null)
        {
            var nfeLocal = nfe;
            if (nfeLocal == null) throw new ArgumentNullException("nfe");

            var config = cfgServico ?? ConfiguracaoServico.Instancia;

            #region Define cNF

            var tamanhocNf = 9;
            var versao = (decimal.Parse(nfeLocal.infNFe.versao, CultureInfo.InvariantCulture));
            if (versao >= 2) tamanhocNf = 8;
            nfeLocal.infNFe.ide.cNF = Convert.ToInt32(nfeLocal.infNFe.ide.cNF).ToString().PadLeft(tamanhocNf, '0');

            #endregion

            var modeloDocumentoFiscal = nfeLocal.infNFe.ide.mod;
            var tipoEmissao = (int)nfeLocal.infNFe.ide.tpEmis;
            var codigoNumerico = int.Parse(nfeLocal.infNFe.ide.cNF);
            var estado = nfeLocal.infNFe.ide.cUF;
            var dataEHoraEmissao = nfeLocal.infNFe.ide.dhEmi;
            var cnpj = nfeLocal.infNFe.emit.CNPJ;

            if (cnpj == null)
            {
                cnpj = nfeLocal.infNFe.emit.CPF.PadLeft(14, '0');
            }

            var numeroDocumento = nfeLocal.infNFe.ide.nNF;
            var serie = nfeLocal.infNFe.ide.serie;

            var dadosChave = ChaveFiscal.ObterChave(estado, dataEHoraEmissao, cnpj, modeloDocumentoFiscal, serie, numeroDocumento, tipoEmissao, codigoNumerico);

            nfeLocal.infNFe.Id = "NFe" + dadosChave.Chave;
            nfeLocal.infNFe.ide.cDV = Convert.ToInt32(dadosChave.DigitoVerificador);

            Signature assinatura = null;
            if (_certificado == null)
                assinatura = Assinador.ObterAssinatura(nfeLocal, nfeLocal.infNFe.Id, config);
            else
                assinatura = Assinador.ObterAssinatura(nfeLocal, nfeLocal.infNFe.Id, _certificado, config.Certificado.ManterDadosEmCache, config.Certificado.SignatureMethodSignedXml, config.Certificado.DigestMethodReference, config.RemoverAcentos);
            nfeLocal.Signature = assinatura;
            return nfeLocal;
        }
    }
}