using System;
using System.Text;
using CTe.Classes.Ext;
using CTeDLL.Classes.Servicos.Consulta;
using CTeDLL.Classes.Servicos.Inutilizacao;
using CTeDLL.Classes.Servicos.Status;
using CTeDLL.Servicos.Inutilizacao;
using DFe.Classes.Extencoes;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Utils.Annotations;

namespace CTeDLL.Servicos.Factory
{
    public class ClassesFactory
    {
        public static consStatServCte CriaConsStatServCte()
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            return new consStatServCte
            {
                versao = configuracaoServico.VersaoLayout,
                tpAmb = configuracaoServico.tpAmb
            };
        }

        public static consSitCTe CriarconsSitCTe(string chave)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            return new consSitCTe
            {
                tpAmb = configuracaoServico.tpAmb,
                versao = configuracaoServico.VersaoLayout,
                chCTe = chave
            };
        }

        public static inutCTe CriaInutCTe(ConfigInutiliza configInutiliza)
        {
            if (configInutiliza == null) throw new ArgumentNullException("Preciso de uma configuração de inutilização");

            var configuracaoServico = ConfiguracaoServico.Instancia;

            var id = new StringBuilder("ID");
            id.Append(configuracaoServico.cUF.GetCodigoIbgeEmString());
            id.Append(configInutiliza.Cnpj);
            id.Append((byte) configInutiliza.ModeloDocumento);
            id.Append(configInutiliza.Serie.ToString("D3"));
            id.Append(configInutiliza.NumeroInicial.ToString("D9"));
            id.Append(configInutiliza.NumeroFinal.ToString("D9"));

            return new inutCTe
            {
                versao = configuracaoServico.VersaoLayout,
                infInut = new infInutEnv
                {
                    Id = id.ToString(),
                    tpAmb = configuracaoServico.tpAmb,
                    cUF = configuracaoServico.cUF,
                    CNPJ = configInutiliza.Cnpj,
                    ano = configInutiliza.Ano,
                    nCTIni = configInutiliza.NumeroInicial,
                    nCTFin = configInutiliza.NumeroInicial,
                    mod = configInutiliza.ModeloDocumento,
                    serie = configInutiliza.Serie,
                    xJust = configInutiliza.Justificativa,
                }
            };
        }
    }
}