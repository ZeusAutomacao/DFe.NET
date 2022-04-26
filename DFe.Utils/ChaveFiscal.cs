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
            var chave = new StringBuilder();

            chave.Append(((int)ufEmitente).ToString("D2"))
                .Append(dataEmissao.ToString("yyMM"))
                .Append(cnpjEmitente)
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
                var ch = Convert.ToInt32(chave[i].ToString());
                soma += ch*peso;
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