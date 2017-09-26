using System.Collections.Generic;
using DFe.CertificadosDigitais;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Flags;

namespace DFe.DocumentosEletronicos.NFe.Servicos.EnviarNFe
{
    public class NFeEnviar
    {
        public NFeEnviar(NFeBaseConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            
        }

        public void Enviar(int idLote, List<Classes.Informacoes.NFe> nfes, IndicadorSincronizacao indSinc, bool compactarMensagem)
        {
            
        }
    }
}