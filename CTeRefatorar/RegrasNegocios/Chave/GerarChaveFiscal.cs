using System;
using System.Text;

namespace FusionCore.DFe.RegrasNegocios.Chave
{
    public class GerarChaveFiscal
    {
        private readonly int _modeloDocumentoFiscal;
        private readonly int _tipoEmissao;
        private readonly long _codigoNumerico;
        private readonly int _codigoIbgeUf;
        private readonly long _documentoUnico;
        private readonly long _numeroDocumento;
        private readonly int _serie;
        private readonly DateTime _dataEHoraEmissao;
        public string Chave { get; private set; }
        public byte DigitoVerificador { get; private set; }

        public GerarChaveFiscal(int modeloDocumentoFiscal,
            int tipoEmissao,
            long codigoNumerico,
            int codigoIbgeUf,
            DateTime dataEHoraEmissao,
            long documentoUnico,
            long numeroDocumento,
            int serie)
        {
            _modeloDocumentoFiscal = modeloDocumentoFiscal;
            _tipoEmissao = tipoEmissao;
            _codigoNumerico = codigoNumerico;
            _codigoIbgeUf = codigoIbgeUf;
            _documentoUnico = documentoUnico;
            _numeroDocumento = numeroDocumento;
            _serie = serie;
            _dataEHoraEmissao = dataEHoraEmissao;


            GeraChave();
        }

        private void GeraChave()
        {
            var codigoUf = _codigoIbgeUf.ToString("D2");
            var anoMes = _dataEHoraEmissao.ToString("yyMM");
            var documentoUnico = _documentoUnico.ToString("D14");
            var modeloDocumentoFiscal = _modeloDocumentoFiscal.ToString("D2");
            var serie = _serie.ToString("D3");
            var numeroDocumento = _numeroDocumento.ToString("D9");
            var tipoEmissao = _tipoEmissao.ToString();
            var codigoNumerico = _codigoNumerico.ToString("D8");

            var chave = new StringBuilder();

            chave.Append(codigoUf)
                .Append(anoMes)
                .Append(documentoUnico)
                .Append(modeloDocumentoFiscal)
                .Append(serie)
                .Append(numeroDocumento)
                .Append(tipoEmissao)
                .Append(codigoNumerico);

            var digitoVerificador = CalculaDigitoVerificador(chave.ToString());

            chave.Append(digitoVerificador);

            Chave = chave.ToString();
            DigitoVerificador = byte.Parse(digitoVerificador);
        }

        private string CalculaDigitoVerificador(string chave)
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

        public static void ValidarChave(string chaveNfe)
        {
            var codifoUF = int.Parse(chaveNfe.Substring(0, 2));

            var anoMes = chaveNfe.Substring(2, 4);
            var ano = int.Parse(anoMes.Substring(0, 2));
            var mes = int.Parse(anoMes.Substring(2, 2));

            var anoEMesData = new DateTime(ano, mes, 1);

            var cnpj = long.Parse(chaveNfe.Substring(6, 14));
            var modelo = int.Parse(chaveNfe.Substring(20, 2));
            var serie = int.Parse(chaveNfe.Substring(22, 3));
            var numeroNfe = long.Parse(chaveNfe.Substring(25, 9));
            var formaEmissao = int.Parse(chaveNfe.Substring(34, 1));
            var codigoNumerico = long.Parse(chaveNfe.Substring(35, 8));
            var digitoVerificador = chaveNfe.Substring(43, 1);

            var gerarChave = new GerarChaveFiscal(modelo,
                formaEmissao,
                codigoNumerico,
                codifoUF,
                anoEMesData,
                cnpj,
                numeroNfe,
                serie);

            if (digitoVerificador.Equals(gerarChave.DigitoVerificador.ToString())) return;

            throw new InvalidOperationException("Chave da NF-e não é válida");
        }
    }
}