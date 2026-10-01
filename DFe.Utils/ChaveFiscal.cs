/********************************************************************************/
/* Projeto: Biblioteca ZeusDFe                                                  */
/* Biblioteca C# para auxiliar no desenvolvimento das demais bibliotecas DFe    */
/*                                                                              */
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
using System.Text;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace DFe.Utils
{
    /// <summary>
    /// Classe com métodos para tratamento da chave dos documentos fiscais
    /// </summary>
    public class ChaveFiscal
    {
        /// <summary>
        /// Obtém a chave do documento fiscal
        /// </summary>
        /// <param name="ufEmitente">UF do emitente do DF-e</param>
        /// <param name="dataEmissao">Data de emissão do DF-e</param>
        /// <param name="cnpjEmitente">CNPJ do emitente do DF-e</param>
        /// <param name="modelo">Modelo do DF-e</param>
        /// <param name="serie">Série do DF-e</param>
        /// <param name="numero">Numero do DF-e</param>
        /// <param name="tipoEmissao">Tipo de emissão do DF-e. Informar inteiro conforme consta no manual de orientação do contribuinte para o DF-e</param>
        /// <param name="cNf">Código numérico que compõe a Chave de Acesso. Número gerado pelo emitente para cada DF-e</param>
        /// <returns>Retorna um objeto <see cref="DadosChaveFiscal"/> com os dados da chave de acesso</returns>
        public static DadosChaveFiscal ObterChave(Estado ufEmitente, DateTimeOffset dataEmissao, string cnpjEmitente, ModeloDocumento modelo, int serie, long numero, int tipoEmissao, int cNf)
        {
            if (string.IsNullOrEmpty(cnpjEmitente))
                throw new ArgumentException("O CNPJ/CPF do emitente deve ser informado.", "cnpjEmitente");

            // NT Conjunta 2025.001: o CNPJ pode ser alfanumérico ([A-Z0-9]{12}[0-9]{2}) e ocupa 14 posições na chave.
            // Somente CPF (11 posições) é completado com zeros à esquerda; qualquer outro comprimento seria um
            // documento truncado e geraria uma chave bem-formada porém errada.
            var documentoEmitente = cnpjEmitente.Length == 11 ? cnpjEmitente.PadLeft(14, '0') : cnpjEmitente;

            if (documentoEmitente.Length != 14)
                throw new ArgumentException(
                    string.Format("O documento do emitente deve ter 14 posições (CNPJ) ou 11 posições (CPF); o valor informado \"{0}\" tem {1}.", cnpjEmitente, cnpjEmitente.Length),
                    "cnpjEmitente");

            var chave = new StringBuilder();

            chave.Append(((int)ufEmitente).ToString("D2"))
                 .Append(dataEmissao.ToString("yyMM"))
                 .Append(documentoEmitente)
                 .Append(((int)modelo).ToString("D2"))
                 .Append(serie.ToString("D3"))
                 .Append(numero.ToString("D9"))
                 .Append(tipoEmissao.ToString())
                 .Append(cNf.ToString("D8"));

            var digitoVerificador = ObterDigitoVerificador(chave.ToString());

            chave.Append(digitoVerificador);

            return new DadosChaveFiscal(chave.ToString(), byte.Parse(digitoVerificador));
        }

        /// <summary>
        /// Calcula e devolve o dígito verificador da chave do DF-e
        /// </summary>
        /// <param name="chave"></param>
        /// <returns></returns>
        private static string ObterDigitoVerificador(string chave)
        {
            var soma = 0; // Vai guardar a Soma
            var mod = -1; // Vai guardar o Resto da divisão
            var dv = -1; // Vai guardar o DigitoVerificador
            var peso = 2; // vai guardar o peso de multiplicação

            //percorrendo cada caractere da chave da direita para esquerda para fazer os cálculos com o peso
            for (var i = chave.Length - 1; i != -1; i--)
            {
                var valorCaractere = ObterValorDoCaractere(chave[i]);
                soma += valorCaractere * peso;
                //sempre que for 9 voltamos o peso a 2
                if (peso < 9)
                    peso += 1;
                else
                    peso = 2;
            }

            //Agora que tenho a soma vamos pegar o resto da divisão por 11
            mod = soma%11;
            //Aqui temos uma regrinha, se o resto da divisão for 0 ou 1 então o dv vai ser 0
            if (mod == 0 || mod == 1)
                dv = 0;
            else
                dv = 11 - mod;

            return dv.ToString();
        }

        /// <summary>
        /// Obtém o valor numérico de um caractere da chave para o cálculo do dígito verificador,
        /// conforme a NT Conjunta 2025.001: valor = código ASCII - 48 ('0'-'9' => 0 a 9; 'A'-'Z' => 17 a 42)
        /// </summary>
        /// <param name="caractere"></param>
        /// <returns></returns>
        internal static int ObterValorDoCaractere(char caractere)
        {
            // A chave admite somente dígitos e letras maiúsculas; qualquer outro caractere
            // (minúsculas, símbolos, ASCII 58-64) produziria um DV errado sem nenhum erro.
            if ((caractere < '0' || caractere > '9') && (caractere < 'A' || caractere > 'Z'))
                throw new ArgumentException(
                    string.Format("Caractere inválido na chave do DF-e: '{0}'. São aceitos somente dígitos (0-9) e letras maiúsculas (A-Z).", caractere));

            return caractere - '0';
        }

        /// <summary>
        /// Informa se a chave de um DF-e é válida
        /// </summary>
        /// <param name="chaveNfe"></param>
        /// <returns></returns>
        public static bool ChaveValida(string chaveNfe)
        {
            Estado codigo;
            Enum.TryParse(chaveNfe.Substring(0, 2), out codigo);

            var anoMes = chaveNfe.Substring(2, 4);

            var ano = int.Parse(anoMes.Substring(0, 2));
            var mes = int.Parse(anoMes.Substring(2, 2));
            var anoEMesData = new DateTime(ano, mes, 1);

            var cnpj = chaveNfe.Substring(6, 14);
            ModeloDocumento modelo;
            Enum.TryParse(chaveNfe.Substring(20, 2), out modelo);
            var serie = int.Parse(chaveNfe.Substring(22, 3));
            var numeroNfe = long.Parse(chaveNfe.Substring(25, 9));
            var formaEmissao = int.Parse(chaveNfe.Substring(34, 1));
            var codigoNumerico = int.Parse(chaveNfe.Substring(35, 8));
            var digitoVerificador = chaveNfe.Substring(43, 1);

            var gerarChave = ObterChave(codigo, anoEMesData, cnpj, modelo, serie, numeroNfe, formaEmissao, codigoNumerico);

            return digitoVerificador.Equals(gerarChave.DigitoVerificador.ToString());
        }
    }

    /// <summary>
    /// Classe com dados da Chave do DF-e
    /// </summary>
    public class DadosChaveFiscal
    {
        public DadosChaveFiscal(string chave, byte digitoVerificador)
        {
            Chave = chave;
            DigitoVerificador = digitoVerificador;
        }

        /// <summary>
        /// Chave de acesso do DF-e
        /// </summary>
        public string Chave { get; private set; }

        /// <summary>
        /// Dígito verificador da chave de acesso do DF-e
        /// </summary>
        public byte DigitoVerificador { get; private set; }
    }
}