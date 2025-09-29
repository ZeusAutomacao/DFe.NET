using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NFe.Danfe.PdfClown.Esquemas
{
    public class Versao
    {
        private const string _Pattern = @"(\d+)\.(\d+)";
        public int Maior { get; protected set; }
        public int Menor { get; protected set; }

        public Versao(int maior, int menor)
        {
            Maior = maior;
            Menor = menor;
        }

        [DebuggerStepThrough]
        public static Versao Parse(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException("O parâmetro str não pode ser nulo ou vazio.", "str");
            }

            Match m = Regex.Match(str, _Pattern);
            Versao v = new Versao(0, 0);

            if (!m.Success)
            {
                throw new ArgumentException("A versão não pode ser interpretada.", "str");
            }

            v.Maior = int.Parse(m.Groups[1].Value);
            v.Menor = int.Parse(m.Groups[2].Value);

            return v;
        }
    }
}
