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
using System.Linq;

namespace NFe.Classes.Informacoes.Destinatario
{
    public class enderDest
    {
        private string _cep;

        /// <summary>
        ///     E06 - Logradouro
        /// </summary>
        public string xLgr { get; set; }

        /// <summary>
        ///     E07 - Número
        /// </summary>
        public string nro { get; set; }

        /// <summary>
        ///     E08 - Complemento
        /// </summary>
        public string xCpl { get; set; }

        /// <summary>
        ///     E09 - Bairro
        /// </summary>
        public string xBairro { get; set; }

        /// <summary>
        ///     E10 - Código do município
        ///     <para>Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.</para>
        /// </summary>
        public long cMun { get; set; }

        /// <summary>
        ///     E11 - Nome do município, informar EXTERIOR para operações com o exterior.
        /// </summary>
        public string xMun { get; set; }

        /// <summary>
        ///     E12 - Sigla da UF, informar EX para operações com o exterior.
        /// </summary>
        public string UF { get; set; }

        /// <summary>
        ///     E13 - Código do CEP
        ///     <para>Informar os zeros não significativos. (NT 2011/004)</para>
        /// </summary>
        public string CEP
        {
            get { return _cep; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("-", "");
                    if (!value.All(char.IsDigit))
                        throw new Exception(@"enderDest\CEP deve receber somente números!");
                    if (value.Length != 8)
                        throw new Exception(string.Format(@"enderDest\CEP deve ter 8 números. Tamanho informado: {0}!", value.Length));
                }
                _cep = value;
            }
        }

        /// <summary>
        ///     E14 - Código do País
        ///     <para>1058 - Brasil</para>
        /// </summary>
        public int? cPais { get; set; }

        /// <summary>
        ///     E15 - Nome do País
        ///     <para>Brasil ou BRASIL</para>
        /// </summary>
        public string xPais { get; set; }

        /// <summary>
        ///     E16 - Telefone
        ///     <para>
        ///         Preencher com o Código DDD + número do telefone. Nas operações com exterior é permitido informar o código do
        ///         país + código da localidade + número do telefone (v.2.0)
        ///     </para>
        /// </summary>
        public long? fone { get; set; }

        public bool ShouldSerializecPais()
        {
            return cPais.HasValue;
        }

        public bool ShouldSerializefone()
        {
            return fone.HasValue;
        }

        public bool ShouldSerializexCpl()
        {
            return !string.IsNullOrEmpty(xCpl);
        }
    }
}