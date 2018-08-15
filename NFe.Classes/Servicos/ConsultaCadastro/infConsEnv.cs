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

namespace NFe.Classes.Servicos.ConsultaCadastro
{
    public class infConsEnv
    {
        private const string ErroCpfCnpjIePreenchidos = "Somente preencher um dos campos: CNPJ, CPF ou IE, para um objeto do tipo infConsEnv!";
        private string _cnpj;
        private string _cpf;
        private string _ie;

        public infConsEnv()
        {
            xServ = "CONS-CAD";
        }

        /// <summary>
        ///     GP04 - Serviço solicitado "CONS-CAD"
        /// </summary>
        public string xServ { get; set; }

        /// <summary>
        ///     GP05 - Sigla da UF consultada, informar 'SU' para SUFRAMA.
        /// </summary>
        public string UF { get; set; }

        /// <summary>
        ///     GP06 - Inscrição estadual do contribuinte
        ///     <para>Somente preencher um dos campos: CNPJ, CPF ou IE</para>
        /// </summary>
        public string IE
        {
            get { return _ie; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(CNPJ) & string.IsNullOrEmpty(CPF))
                    _ie = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjIePreenchidos);
                }
            }
        }

        /// <summary>
        ///     GP07 - CNPJ do contribuinte
        ///     <para>Somente preencher um dos campos: CNPJ, CPF ou IE</para>
        /// </summary>
        public string CNPJ
        {
            get { return _cnpj; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(CPF) & string.IsNullOrEmpty(IE))
                    _cnpj = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjIePreenchidos);
                }
            }
        }

        /// <summary>
        ///     GP08 - CPF do contribuinte
        ///     <para>Somente preencher um dos campos: CNPJ, CPF ou IE</para>
        /// </summary>
        public string CPF
        {
            get { return _cpf; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(CNPJ) & string.IsNullOrEmpty(IE))
                    _cpf = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjIePreenchidos);
                }
            }
        }
    }
}