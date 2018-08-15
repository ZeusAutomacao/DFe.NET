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
using System.Text.RegularExpressions;

namespace NFe.Classes.Informacoes.Emitente
{
    public class emit
    {
        private const string ErroCpfCnpjPreenchidos = "Somente preencher um dos campos: CNPJ ou CPF, para um objeto do tipo emit!";
        private string _cnpj;
        private string _cpf;

        /// <summary>
        ///     C02 - CNPJ do emitente
        /// </summary>
        public string CNPJ
        {
            get { return _cnpj; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(_cpf))
                    _cnpj = Regex.Match(value, @"\d+").Value;

                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        ///     C02a - CPF do remetente
        /// </summary>
        public string CPF
        {
            get { return _cpf; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(_cnpj))
                    _cpf = Regex.Match(value, @"\d+").Value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        ///     C03 - Razão Social ou Nome do emitente
        /// </summary>
        public string xNome { get; set; }

        /// <summary>
        ///     C04 - Nome fantasia
        /// </summary>
        public string xFant { get; set; }

        /// <summary>
        ///     C05 - Grupo do Endereço do emitente
        /// </summary>
        public enderEmit enderEmit { get; set; }

        /// <summary>
        ///     C17 - Inscrição Estadual
        ///     <para>Campo de informação obrigatória nos casos de emissão própria (procEmi = 0, 2 ou 3).</para>
        ///     <para>
        ///         A IE deve ser informada apenas com algarismos para destinatários contribuintes do ICMS, sem caracteres de
        ///         formatação (ponto, barra, hífen, etc.);
        ///     </para>
        ///     <para>
        ///         O literal “ISENTO” deve ser informado apenas para contribuintes do ICMS que são isentos de inscrição no
        ///         cadastro de contribuintes do ICMS e estejam emitindo NF-e avulsa;
        ///     </para>
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        ///     C18 - IE do Substituto Tributário
        ///     <para>Informar a IE do ST da UF de destino da mercadoria, quando houver a retenção do ICMS ST para a UF de destino.</para>
        /// </summary>
        public string IEST { get; set; }

        /// <summary>
        ///     C19 - Inscrição Municipal
        ///     <para>
        ///         Este campo deve ser informado, quando ocorrer a emissão de NF-e conjugada, com prestação de serviços sujeitos
        ///         ao ISSQN e fornecimento de peças sujeitos ao ICMS.
        ///     </para>
        /// </summary>
        public string IM { get; set; }

        /// <summary>
        ///     C20 - CNAE fiscal
        ///     <para>Este campo deve ser informado quando o campo IM (C19) for informado.</para>
        /// </summary>
        public string CNAE { get; set; }

        /// <summary>
        ///     C21 - Código de Regime Tributário
        /// </summary>
        public CRT CRT { get; set; }

    }
}