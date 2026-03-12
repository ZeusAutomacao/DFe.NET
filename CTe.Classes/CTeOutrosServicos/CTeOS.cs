using System;
using System.Xml.Serialization;
using CTe.Classes;
using CTe.Classes.Informacoes;
using DFe.Classes.Assinatura;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using CTe.CTeOSDocumento.CTe.CTeOS.Informacoes;
using DFe.Utils;
using DFe.Utils.Assinatura;

namespace CTe.CTeOSClasses
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class CTeOS
    {
        public CTeOS()
        {
            versao = VersaoServico.Versao400;
        }

        [XmlAttribute]
        public VersaoServico versao { get; set; }

        [XmlElement(ElementName = "infCte", Namespace = "http://www.portalfiscal.inf.br/cte")]
        public infCteOS InfCte { get; set; }

        [XmlElement(ElementName = "infCTeSupl")]
        public infCTeSupl infCTeSupl { get; set; }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        public static CTeOS LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<CTeOS>(xml);
        }

        public static CTeOS LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<CTeOS>(caminhoArquivoXml);
        }

        [System.Obsolete("Use IServicosCTe.Assina(cteOs) em vez de chamar cteOs.Assina() diretamente. Este método será removido em versão futura.")]
        public virtual void Assina(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var modeloDocumentoFiscal = InfCte.ide.mod;
            var tipoEmissao = (int)InfCte.ide.tpEmis;
            var codigoNumerico = InfCte.ide.cCT;
            var estado = InfCte.ide.cUF;
            var dataEHoraEmissao = InfCte.ide.dhEmi;
            var cnpj = InfCte.emit.CNPJ;
            var numeroDocumento = InfCte.ide.nCT;
            int serie = InfCte.ide.serie;

            var dadosChave = ChaveFiscal.ObterChave(estado, dataEHoraEmissao, cnpj, modeloDocumentoFiscal,
                serie, numeroDocumento, tipoEmissao, codigoNumerico);

            InfCte.Id = "CTe" + dadosChave.Chave;
            InfCte.versao = DFe.Classes.Flags.VersaoServico.Versao400;
            InfCte.ide.cDV = dadosChave.DigitoVerificador;

            var assinatura = AssinaturaDigital.Assina(this, InfCte.Id, configServico.X509Certificate2);

            Signature = assinatura;
        }
    }
}