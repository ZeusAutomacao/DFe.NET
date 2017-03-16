using System.Collections.Generic;
using CTeDLL.Classes.Servicos.Recepcao;
using CTeDLL.Servicos.Factory;
using CTeDLL.Utils.CTe;
using CTeDLL.Utils.Recepcao;
using DFe.Classes.Flags;
using CTeEletronico = CTe.Classes.CTe;

namespace CTeDLL.Servicos.Recepcao
{
    public class ServicoCTeRecepcao
    {
        public retEnviCte CTeRecepcao(int lote, List<CTeEletronico> cteEletronicosList)
        {
            var instanciaConfiguracao = ConfiguracaoServico.Instancia;

            var enviCte = ClassesFactory.CriaEnviCTe(lote, cteEletronicosList);

            if (instanciaConfiguracao.tpAmb == TipoAmbiente.Homologacao)
            {
                foreach (var cte in enviCte.CTe)
                {
                    const string razaoSocial = "CT-E EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                    cte.infCte.rem.xNome = razaoSocial;
                    cte.infCte.dest.xNome = razaoSocial;
                }
            }


            foreach (var cte in enviCte.CTe)
            {
                cte.Assina();
                cte.ValidaSchema();
            }

            enviCte.ValidaSchema();
            enviCte.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlCteRecepcao();
            var retornoXml = webService.cteRecepcaoLote(enviCte.CriaRequestWs());

            var retorno = retEnviCte.LoadXml(retornoXml.OuterXml, enviCte);
            retorno.SalvarXmlEmDisco();

            return retorno;
        }
    }
}