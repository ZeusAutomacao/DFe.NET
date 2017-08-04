using System;
using System.IO;
using System.Text;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe;

namespace DFe.ResolvePastas
{
    public class ResolvePasta
    {
        private DFeConfig DfeConfig { get; }
        private DateTime DataEnvioWs { get; }

        public ResolvePasta(DFeConfig dfeConfig, DateTime dataEnvioWS)
        {
            DfeConfig = dfeConfig;
            DataEnvioWs = dataEnvioWS;
        }

        public string PastaEnviar()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Autorizar\Enviado");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaEnviarLote()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Lote\Enviado");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaRetornoEnviados()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Autorizar\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaNaoEncerradoEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\NaoEncerrados\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaNaoEncerradoRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\NaoEncerrados\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaConsultaProtocoloEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\ConsultaProtocolo\Enviado");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaConsultaProtocoloRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\ConsultaProtocolo\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaCanceladosEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Cancelados\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaCanceladosRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Cancelados\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaEncerramentoEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Encerramento\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaEncerramentoRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Encerramento\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaIncluirCondutorEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\IncluirCondutor\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaIncluirCondutorRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\IncluirCondutor\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaRegistroDePassagemEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\RegistroDePassagem\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaRegistroDePassagemRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\RegistroDePassagem\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaConsultaLoteEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\ConsultaLote\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaConsultaLoteRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\ConsultaLote\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaConsultaStatusEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\ConsultaStatus\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaConsultaStatusRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\ConsultaStatus\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        private static void CriaDiretorioSeNaoExistir(StringBuilder caminhoXml)
        {
            if (!Directory.Exists(caminhoXml.ToString()))
            {
                Directory.CreateDirectory(caminhoXml.ToString());
            }
        }

        private StringBuilder CriaDiretorio()
        {
            var caminhoXml = new StringBuilder(DfeConfig.CaminhoSalvarXml);

            caminhoXml.Append(@"\");
            caminhoXml.Append(DfeConfig.CnpjEmitente);

            caminhoXml.Append(@"\");
            caminhoXml.Append(DataEnvioWs.ToString("MMMM"));
            return caminhoXml;
        }

        public string PastaRetornoEnviadosProc()
        {
            var caminho = new StringBuilder(PastaRetornoEnviados());

            caminho.Append(@"\Proc");

            return caminho.ToString();
        }

        public string PastaCartaCorrecaoEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\CartaCorrecao\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaCartaCorrecaoRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\CartaCorrecao\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaInutilizacaoEnvio()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Inutilizacao\Envio");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }

        public string PastaInutilizacaoRetorno()
        {
            var caminhoXml = CriaDiretorio();

            caminhoXml.Append(@"\Inutilizacao\Retorno");

            CriaDiretorioSeNaoExistir(caminhoXml);

            return caminhoXml.ToString();
        }
    }
}