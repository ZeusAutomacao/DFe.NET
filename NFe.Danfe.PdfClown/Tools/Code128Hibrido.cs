using System.Text.RegularExpressions;

namespace NFe.Danfe.PdfClown.Tools
{
    /// <summary>
    /// Codificador CODE-128 híbrido (subconjuntos C e A) conforme a seção 6 da NT Conjunta 2025.001,
    /// para o código de barras da chave de acesso com CNPJ alfanumérico.
    /// Regras: inicia com Start C (105); alterna para o Code A (código 101) nos caracteres não numéricos;
    /// retorna ao Code C (código 99) em corridas de 4 ou mais dígitos ou em corrida final com quantidade
    /// par de dígitos; dígito ímpar remanescente antes de uma letra fica no Code A.
    /// Conteúdo 100% numérico produz exatamente a sequência do CODE-128 C puro.
    /// </summary>
    public static class Code128Hibrido
    {
        private const byte StartC = 105;
        private const byte TrocaParaCodeA = 101;
        private const byte TrocaParaCodeC = 99;
        private const byte Stop = 106;

        /// <summary>
        /// Obtém a sequência completa de símbolos CODE-128 (Start C, dados com as trocas de subconjunto,
        /// dígito verificador módulo 103 e Stop) para um conteúdo composto por dígitos e letras maiúsculas.
        /// </summary>
        /// <param name="codigo">Conteúdo a codificar ([0-9A-Z]+), ex.: chave de acesso de 44 posições</param>
        /// <returns>Símbolos CODE-128, um valor (0-106) por posição</returns>
        public static byte[] ObterSimbolos(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                throw new ArgumentException("O código não pode ser vazio.", nameof(codigo));

            if (!Regex.IsMatch(codigo, "^[0-9A-Z]+$"))
                throw new ArgumentException("O código deve conter somente dígitos (0-9) e letras maiúsculas (A-Z).", nameof(codigo));

            var simbolos = new List<byte> { StartC };
            var emCodeC = true;
            var i = 0;

            while (i < codigo.Length)
            {
                var corrida = TamanhoCorridaDeDigitos(codigo, i);

                if (corrida == 0)
                {
                    //caractere não numérico: garante o Code A e codifica a letra
                    if (emCodeC)
                    {
                        simbolos.Add(TrocaParaCodeA);
                        emCodeC = false;
                    }

                    simbolos.Add(ValorCodeA(codigo[i]));
                    i++;
                    continue;
                }

                var corridaFinal = i + corrida == codigo.Length;

                if (emCodeC)
                {
                    //em Code C codifica os dígitos aos pares; o dígito ímpar remanescente fica no Code A
                    for (var p = 0; p < corrida / 2; p++)
                    {
                        simbolos.Add(ValorCodeC(codigo, i));
                        i += 2;
                    }

                    if (corrida % 2 == 1)
                    {
                        simbolos.Add(TrocaParaCodeA);
                        emCodeC = false;
                        simbolos.Add(ValorCodeA(codigo[i]));
                        i++;
                    }
                }
                else if (corrida >= 4 || (corridaFinal && corrida % 2 == 0))
                {
                    //retorna ao Code C para a parte com quantidade par de dígitos
                    if (corrida % 2 == 1 && corridaFinal)
                    {
                        //corrida final ímpar de 4+ dígitos: o primeiro dígito fica no Code A para a parte restante ser par
                        simbolos.Add(ValorCodeA(codigo[i]));
                        i++;
                        corrida--;
                    }

                    simbolos.Add(TrocaParaCodeC);
                    emCodeC = true;

                    for (var p = 0; p < corrida / 2; p++)
                    {
                        simbolos.Add(ValorCodeC(codigo, i));
                        i += 2;
                    }

                    //corrida ímpar antes de letra: o dígito remanescente é codificado no Code A na próxima iteração
                }
                else
                {
                    //corrida curta (1 a 3 dígitos) que não justifica a troca: codifica os dígitos no próprio Code A
                    for (var d = 0; d < corrida; d++)
                    {
                        simbolos.Add(ValorCodeA(codigo[i]));
                        i++;
                    }
                }
            }

            simbolos.Add(ObterDigitoVerificador(simbolos));
            simbolos.Add(Stop);

            return simbolos.ToArray();
        }

        /// <summary>
        /// Dígito verificador módulo 103: soma ponderada dos símbolos, com o start valendo peso 1
        /// e cada símbolo de dados o peso da sua posição
        /// </summary>
        private static byte ObterDigitoVerificador(List<byte> simbolos)
        {
            var soma = (int)simbolos[0];

            for (var i = 1; i < simbolos.Count; i++)
                soma += i * simbolos[i];

            return (byte)(soma % 103);
        }

        private static int TamanhoCorridaDeDigitos(string codigo, int inicio)
        {
            var fim = inicio;

            while (fim < codigo.Length && codigo[fim] >= '0' && codigo[fim] <= '9')
                fim++;

            return fim - inicio;
        }

        private static byte ValorCodeC(string codigo, int posicao)
        {
            return (byte)((codigo[posicao] - '0') * 10 + (codigo[posicao + 1] - '0'));
        }

        private static byte ValorCodeA(char caractere)
        {
            //no Code A os caracteres ASCII 32-95 valem ASCII - 32 ('0'-'9' => 16-25; 'A'-'Z' => 33-58)
            return (byte)(caractere - 32);
        }
    }
}
