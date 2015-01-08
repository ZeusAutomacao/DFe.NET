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
using System.Text.RegularExpressions;
using NFe.Classes.Informacoes;

namespace NFe.Utils
{
    public static class Gerador
    {
        public static string GerarChave(infNFe infNFe)
        {
            var cpfcnpj = String.IsNullOrEmpty(infNFe.emit.CNPJ) ? Regex.Match(infNFe.emit.CPF, @"\d+").Value : Regex.Match(infNFe.emit.CNPJ, @"\d+").Value;

            var tipoEmissao = "";
            var tamanhocNf = 9;
            var anoMesEmissao = Convert.ToDateTime(infNFe.ide.dhEmi).ToString("yyMM");

            if (Decimal.Parse(infNFe.versao, CultureInfo.InvariantCulture) == 2) //Se a versão for 2, usar o campo dEmi ao invés de dhEmi
                anoMesEmissao = Convert.ToDateTime(infNFe.ide.dEmi).ToString("yyMM");


            if (Decimal.Parse(infNFe.versao, CultureInfo.InvariantCulture) >= 2) //De acordo com o manual de oriantação v5.0 pág. 92
            {
                tipoEmissao = ((int) infNFe.ide.tpEmis).ToString();
                tamanhocNf = 8;
            }

            var chave =
                ((int) infNFe.ide.cUF).ToString().PadLeft(2, '0') + //cUF - Código da UF do emitente do Documento Fisca
                anoMesEmissao + //AAMM - Ano e Mês de emissão da NF-e
                cpfcnpj.PadLeft(14, '0') + //CNPJ - CNPJ do emitente
                ((int) infNFe.ide.mod).ToString().PadLeft(2, '0') + //mod - Modelo do Documento Fiscal
                infNFe.ide.serie.ToString().PadLeft(3, '0') + //serie - Série do Documento Fiscal
                infNFe.ide.nNF.ToString().PadLeft(9, '0') + //nNF - Número do Documento Fiscal 
                tipoEmissao + //tpEmis – forma de emissão da NF-e
                infNFe.ide.cNF.PadLeft(tamanhocNf, '0'); //cNF - Código Numérico que compõe a Chave de Acesso

            var cDv = GerarDigitoVerificadorNFe(chave);
            return chave + cDv;
        }

        public static string GerarId(string chave)
        {
            return "NFe" + chave;
        }

        private static string GerarDigitoVerificadorNFe(string chave)
        {
            var soma = 0; // Vai guardar a Soma
            var mod = -1; // Vai guardar o Resto da divisão
            var dv = -1; // Vai guardar o DigitoVerificador
            var pesso = 2; // vai guardar o pesso de multiplicacao

            //percorrendo cada caracter da chave da direita para esquerda para fazer os calculos com o pesso
            for (var i = chave.Length - 1; i != -1; i--)
            {
                var ch = Convert.ToInt32(chave[i].ToString());
                soma += ch*pesso;
                //sempre que for 9 voltamos o pesso a 2
                if (pesso < 9)
                    pesso += 1;
                else
                    pesso = 2;
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
    }
}