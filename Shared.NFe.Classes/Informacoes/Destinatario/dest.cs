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
using System.Xml.Serialization;
using DFe.Classes.Flags;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Classes.Informacoes.Destinatario
{
    public class dest
    {
        private const string ErroCpfCnpjPreenchidos = "Somente preencher um dos campos: CNPJ ou CPF, para um objeto do tipo dest!";
        private string cnpj;
        private string cpf;
        private readonly VersaoServico _versao;

        /// <summary>
        ///     A versão do serviço é obrigatória por conta do tratamento que será feito na propriedade IE
        /// </summary>
        /// <param name="versao"></param>
        public dest(VersaoServico versao)
        {
            _versao = versao;
            IE = "";
        }

        internal dest()
        {
        }

        /// <summary>
        ///     E02 - CNPJ do destinatário
        /// </summary>
        public string CNPJ
        {
            get { return cnpj; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(cpf))
                    cnpj = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        ///     E03 - CPF do destinatário
        /// </summary>
        public string CPF
        {
            get { return cpf; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(cnpj))
                    cpf = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        ///     E03a - Identificador do destinatário, em caso de comprador estrangeiro
        /// </summary>
        public string idEstrangeiro { get; set; }

        /// <summary>
        ///     E04 - Razão Social ou nome do destinatário
        /// </summary>
        public string xNome { get; set; }

        /// <summary>
        ///     E05 - Endereço do Destinatário da NF-e
        /// </summary>
        public enderDest enderDest { get; set; }

        /// <summary>
        ///     E16a - Indicador da IE do destinatário:
        /// </summary>
        public indIEDest? indIEDest { get; set; } //Nulable por conta da v2.00

        /// <summary>
        ///     E17 - Inscrição Estadual
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
        [XmlElement(IsNullable = true)]
        public string IE { get; set; }

        /// <summary>
        ///     E18 - Inscrição na SUFRAMA (Obrigatório nas operações com as áreas com benefícios de incentivos fiscais sob
        ///     controle da SUFRAMA)
        /// </summary>
        public string ISUF { get; set; }

        /// <summary>
        ///     E18a - Inscrição Municipal
        ///     <para>
        ///         Este campo deve ser informado, quando ocorrer a emissão de NF-e conjugada, com prestação de serviços sujeitos
        ///         ao ISSQN e fornecimento de peças sujeitos ao ICMS.
        ///     </para>
        /// </summary>
        public string IM { get; set; }

        /// <summary>
        ///     E19 - Informar o e-mail do destinatário. O campo pode ser utilizado para informar o e-mail de recepção da NF-e
        ///     indicada pelo destinatário
        /// </summary>
        public string email { get; set; }

        public bool ShouldSerializeindIEDest()
        {
            return indIEDest.HasValue;
        }

        public bool ShouldSerializeIE()
        {
            var teste = _versao == VersaoServico.Versao200 | !string.IsNullOrEmpty(IE);
            return teste;
        }
    }
}