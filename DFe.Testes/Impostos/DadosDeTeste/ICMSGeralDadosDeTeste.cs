using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Emitente;
using System.Collections.Generic;

namespace DFe.Testes.Impostos.DadosDeTeste
{
    public class ICMSGeralDadosDeTeste
    {
        public static IEnumerable<object[]> ObterRegimesTributariosParaCst61()
        {
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmNacional, 1000, 18, 180 };
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraImportacaoDireta, 1001, 180, 18 };
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraAdquiridaBrasil, 1000, 180, 10 };
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmNacionalConteudoImportacaoSuperior40, 1200, 18, 180 };
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmNacionalProcessosBasicos, 1100, 18, 180 };
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmNacionalConteudoImportacaoInferiorIgual40, 1010, 12, 10 };
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraImportacaoDiretaSemSimilar, 101, 17, 11 };
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmEstrangeiraAdquiridaBrasilSemSimilar, 105, 19, 15 };
            yield return new object[] { CRT.RegimeNormal, OrigemMercadoria.OmNacionalConteudoImportacaoSuperior70, 103, 18, 15 };

            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmNacional, 1000, 18, 180 };
            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmEstrangeiraImportacaoDireta, 1001, 180, 18 };
            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmEstrangeiraAdquiridaBrasil, 1000, 180, 10 };
            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmNacionalConteudoImportacaoSuperior40, 1200, 18, 180 };
            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmNacionalProcessosBasicos, 1100, 18, 180 };
            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmNacionalConteudoImportacaoInferiorIgual40, 1010, 12, 10 };
            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmEstrangeiraImportacaoDiretaSemSimilar, 101, 17, 11 };
            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmEstrangeiraAdquiridaBrasilSemSimilar, 105, 19, 15 };
            yield return new object[] { CRT.SimplesNacional, OrigemMercadoria.OmNacionalConteudoImportacaoSuperior70, 103, 18, 15 };

            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmNacional, 1000, 18, 180 };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmEstrangeiraImportacaoDireta, 1001, 180, 18 };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmEstrangeiraAdquiridaBrasil, 1000, 180, 10 };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmNacionalConteudoImportacaoSuperior40, 1200, 18, 180 };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmNacionalProcessosBasicos, 1100, 18, 180 };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmNacionalConteudoImportacaoInferiorIgual40, 1010, 12, 10 };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmEstrangeiraImportacaoDiretaSemSimilar, 101, 17, 11 };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmEstrangeiraAdquiridaBrasilSemSimilar, 105, 19, 15 };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite, OrigemMercadoria.OmNacionalConteudoImportacaoSuperior70, 103, 18, 15 };
        }

        public static IEnumerable<object[]> ObterRegimesTributarios()
        {
            yield return new object[] { CRT.SimplesNacional };
            yield return new object[] { CRT.SimplesNacionalExcessoSublimite };
            yield return new object[] { CRT.RegimeNormal };
        }
    }
}
