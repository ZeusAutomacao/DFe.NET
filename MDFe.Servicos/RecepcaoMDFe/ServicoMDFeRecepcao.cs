using System;
using DFe.Utils;
using MDFe.Classes.Extencoes;
using MDFe.Classes.Flags;
using MDFe.Classes.Retorno.MDFeRecepcao;
using MDFe.Classes.Retorno.MDFeRetRecepcao.Sincrono;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.RecepcaoMDFe
{
    public class ServicoMDFeRecepcao
    {
        public event EventHandler<AntesDeEnviar> AntesDeEnviar;
        public event EventHandler<string> GerouChave; 

        public MDFeRetEnviMDFe MDFeRecepcao(long lote, MDFeEletronico mdfe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var enviMDFe = ClassesFactory.CriaEnviMDFe(lote, mdfe, config);

            switch (config.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    mdfe.InfMDFe.InfModal.VersaoModal = MDFeVersaoModal.Versao100;
                    mdfe.InfMDFe.Ide.ProxyDhIniViagem = mdfe.InfMDFe.Ide.DhIniViagem.ParaDataHoraStringSemUtc();
                    break;
                case VersaoServico.Versao300:
                    mdfe.InfMDFe.InfModal.VersaoModal = MDFeVersaoModal.Versao300;
                    mdfe.InfMDFe.Ide.ProxyDhIniViagem = mdfe.InfMDFe.Ide.DhIniViagem.ParaDataHoraStringUtc();
                    break;
            }

            enviMDFe.MDFe.Assina(GerouChave, this, config);

            if (config.IsAdicionaQrCode && config.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.infMDFeSupl = mdfe.QrCode(config.X509Certificate2);
            }

            enviMDFe.Valida(config);
            enviMDFe.SalvarXmlEmDisco(config);

            var webService = WsdlFactory.CriaWsdlMDFeRecepcao(config);

            OnAntesDeEnviar(enviMDFe);

            var retornoXml = webService.mdfeRecepcaoLote(enviMDFe.CriaXmlRequestWs());

            var retorno = MDFeRetEnviMDFe.LoadXml(retornoXml.OuterXml, enviMDFe);
            retorno.SalvarXmlEmDisco(config);

            return retorno;
        }

        public MDFeRetMDFe MDFeRecepcaoSinc(MDFeEletronico mdfe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            mdfe.InfMDFe.InfModal.VersaoModal = MDFeVersaoModal.Versao300;
            mdfe.InfMDFe.Ide.ProxyDhIniViagem = mdfe.InfMDFe.Ide.DhIniViagem.ParaDataHoraStringUtc();
            mdfe.Assina(GerouChave, this, config);

            if (config.IsAdicionaQrCode)
            {
                mdfe.infMDFeSupl = mdfe.QrCode(config.X509Certificate2);
            }

            mdfe.Valida(config);
            mdfe.SalvarXmlEmDisco(cfgMdfe: config);

            var webService = WsdlFactory.CriaWsdlMDFeRecepcaoSinc(config);

            OnAntesDeEnviar(new MDFeEnviMDFe
            {
                IdLote = "1",
                MDFe = mdfe,
                Versao = VersaoServico.Versao300
            });

            var retornoXml = webService.mdfeRecepcao(mdfe.CriaXmlRequestWs());

            var retorno = MDFeRetMDFe.LoadXml(retornoXml.OuterXml, mdfe);
            return retorno;           
        }

        protected virtual void OnAntesDeEnviar(MDFeEnviMDFe enviMdfe)
        {
            var handler = AntesDeEnviar;
            if (handler != null) handler(this, new AntesDeEnviar(enviMdfe));
        }
    }
}