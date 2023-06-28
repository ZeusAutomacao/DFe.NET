// ===================================================================
//  Empresa: DSBR - Empresa de Desenvolvimento de Sistemas
//  Projeto: DSBR - Automação Comercial
//  Autores:  Valnei Filho, Vagner Marcelo
//  E-mail: dsbrbrasil@yahoo.com.br
//  Data Criação: 10/04/2020
//  Todos os direitos reservados
// ===================================================================


#region

using System;
using System.Collections.Generic;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Informacoes.Transporte;
using NFe.Danfe.Html.CrossCutting;

#endregion

namespace NFe.Danfe.Html.Dominio
{
    public class DanfeNFe
    {
        #region Propriedades

        public Destinatario Destinatario { get; }

        public Emitente Emitente { get; }

        public string Chave { get; }

        public decimal ValorTotNota { get; }

        public string NumNota { get; }

        public string SerieNota { get; }

        /// <summary>
        ///     Status da nota
        /// </summary>
        public Status Status { get; }

        /// <summary>
        ///     Tipo operação
        ///     <para>0 - Entrada; 1 - Saida</para>
        /// </summary>
        public string TipoOper { get; }

        /// <summary>
        ///     Inscrição estadual do substituto tributário
        /// </summary>
        public string IeSt { get; }

        /// <summary>
        ///     True, ambiente de produção
        /// </summary>
        public bool Producao { get; }

        /// <summary>
        ///     Natureza da operação
        /// </summary>
        public string NatOperacao { get; }

        /// <summary>
        ///     Protocolo de autorização de uso
        /// </summary>
        public string ProtocoloUso { get; }

        /// <summary>
        ///     Data emissão
        /// </summary>
        public DateTime DataEmissao { get; }

        /// <summary>
        ///     Data Entrada Saida
        /// </summary>
        public DateTime? DataEntSaida { get; }

        public ICollection<Fatura> Faturas { get; }

        public Imposto Imposto { get; }

        /// <summary>
        ///     Transportadora
        /// </summary>
        public Transportadora Transportadora { get; }

        /// <summary>
        ///     Volumes
        /// </summary>
        public ICollection<Volume> Volumes { get; }

        /// <summary>
        ///     Produtos
        /// </summary>
        public ICollection<Produto> Produtos { get; }

        /// <summary>
        ///     Dados sobre serviço
        /// </summary>
        public Issqn ISsqn { get; }

        /// <summary>
        ///     Informações adicionais
        /// </summary>
        public InfAdic Inf { get; }

        /// <summary>
        ///     Credito empresa Software
        /// </summary>
        public string Creditos { get; }

        #endregion

        #region Construtor

        ///// <summary>
        ///// </summary>
        ///// <param name="destinatario">Destinatario</param>
        ///// <param name="emitente">Emitente</param>
        ///// <param name="chave">Chave</param>
        ///// <param name="valorTotNota">Valor total da nota</param>
        ///// <param name="numNota">Número da nota</param>
        ///// <param name="serieNota">Série da nota</param>
        ///// <param name="status">Status da nota</param>
        ///// <param name="tipoOper">0 - Entrada; 1 - Saida</param>
        ///// <param name="ieSt">Inscrição estadual</param>
        ///// <param name="natOperacao">Natureza da operação</param>
        ///// <param name="protocoloUso">Protocolo de uso</param>
        ///// <param name="dataEmissao">Data da emissão</param>
        ///// <param name="dataEntSaid">data de saida</param>
        ///// <param name="faturas">Faturas</param>
        ///// <param name="imposto">Impostos</param>
        ///// <param name="transportadora">Transportadora</param>
        ///// <param name="volumes">volumes</param>
        ///// <param name="produtos">Produtos</param>
        ///// <param name="ssqn"></param>
        ///// <param name="inf">Informações adicionais</param>
        
        //public DanfeNFe(Destinatario destinatario, Emitente emitente, string chave, decimal valorTotNota, string numNota, string serieNota,
        //        Status status, string tipoOper, string ieSt, string natOperacao, string protocoloUso, DateTime dataEmissao,
        //        DateTime dataEntSaid, ICollection<Fatura> faturas, Imposto imposto, Transportadora transportadora,
        //        ICollection<Volume> volumes, ICollection<Produto> produtos, Issqn issqn, InfAdic inf, string creditos)
        //{
        //    Destinatario = destinatario;
        //    Emitente = emitente;
        //    Chave = chave;
        //    ValorTotNota = valorTotNota;
        //    NumNota = numNota;
        //    SerieNota = serieNota;
        //    Status = status;
        //    TipoOper = tipoOper;
        //    IeSt = ieSt;
        //    NatOperacao = natOperacao;
        //    ProtocoloUso = protocoloUso;
        //    DataEmissao = dataEmissao;
        //    DataEntSaida = dataEntSaid;
        //    Faturas = faturas;
        //    Imposto = imposto;
        //    Transportadora = transportadora;
        //    Volumes = volumes;
        //    Produtos = produtos;
        //    ISsqn = issqn;
        //    Inf = inf;
        //    Creditos = creditos;
        //}


        /// <summary>
        /// ´
        /// </summary>
        /// <param name="nfe"></param>
        /// <param name="status">Status</param>
        /// <param name="protocolo">Número do protocolo retornado pela SEFAZ</param>
        /// <param name="creditos">Créditos</param>
        /// <param name="issqn">ISSQN</param>
        public DanfeNFe(Classes.NFe nfe, Status status,string protocolo, string creditos, Issqn issqn=null)
        {
            if (nfe == null) throw new ArgumentNullException(nameof(nfe));
            if(nfe.infNFe.ide.mod!=ModeloDocumento.NFe) throw new InvalidOperationException("Modelo da nota imcompatível com o esperado 55");

            #region Emitente

            var doc = nfe.infNFe.emit.CNPJ + nfe.infNFe.emit.CPF;
            var enderecoEmit = new Endereco(nfe.infNFe.emit.enderEmit.xLgr, nfe.infNFe.emit.enderEmit.xBairro, 
                    nfe.infNFe.emit.enderEmit.xMun, nfe.infNFe.emit.enderEmit.nro, nfe.infNFe.emit.enderEmit.CEP,
                    nfe.infNFe.emit.enderEmit.UF.ToString(), nfe.infNFe.emit.enderEmit.fone.ToString(), nfe.infNFe.emit.enderEmit.UF.ToString());
            Emitente = new Emitente(nfe.infNFe.emit.xNome, nfe.infNFe.emit.IE, doc, "", enderecoEmit);

            #endregion

            #region Transportadora

            if (nfe.infNFe.transp?.transporta != null)
            {
                var placa = "";
                var rntc = "";
                 

                if (nfe.infNFe.transp.veicTransp != null)
                {
                    placa = nfe.infNFe.transp.veicTransp.placa;
                    rntc = nfe.infNFe.transp.veicTransp.RNTC;
                }
                var frete = "";
                switch (nfe.infNFe.transp.modFrete)
                {
                    case ModalidadeFrete.mfContaEmitenteOumfContaRemetente:
                        frete = "Remet/Dest";
                        break;
                    case ModalidadeFrete.mfContaDestinatario:
                        frete = "Destinatário";
                        break;
                    case ModalidadeFrete.mfContaTerceiros:
                        frete = "Terceiros";
                        break;
                    case ModalidadeFrete.mfProprioContaRemente:
                        frete = "Remetente";
                        break;
                    case ModalidadeFrete.mfProprioContaDestinatario:
                        frete = "Próprio Destinatário";
                        break;
                    case ModalidadeFrete.mfSemFrete:
                        frete = "Sem Frete";break;

                }

                var doc1 = nfe.infNFe.transp.transporta.CNPJ + nfe.infNFe.transp.transporta.CPF;
                Transportadora = new Transportadora(new Endereco(nfe.infNFe.transp.transporta.xEnder, "",
                                nfe.infNFe.transp.transporta.xMun, "", "", nfe.infNFe.transp.transporta.UF, "", ""),
                        doc1, nfe.infNFe.transp.transporta.xNome, placa, rntc, nfe.infNFe.transp.transporta.IE, frete);

                #region Reboque

                if (nfe.infNFe.transp?.reboque!=null)
                {
                     var reboque = new List<Reboque>();
                     foreach (var item in nfe.infNFe.transp.reboque)
                     {
                         reboque.Add(new Reboque (item.placa,item.RNTC,item.UF));
                     }

                     Transportadora.Reboque = reboque;
                }
                

                #endregion
            }

            
            #region Volume

            if (nfe.infNFe.transp?.vol != null)
            {
                Volumes = new List<Volume>();
                foreach (var item in nfe.infNFe.transp.vol)
                {
                    Volumes.Add(
                        new Volume(item.qVol, item.esp, item.pesoB ?? 0, item.pesoL ?? 0, item.marca, item.nVol));
                }
            }

            #endregion

            #endregion

            #region Destinatario

            Endereco endereco = null;
            var dest = nfe.infNFe.dest;
            if (dest != null)
            {
                var cpfcnpj = dest.CPF + dest.CNPJ;
                if (dest.enderDest != null)
                {
                    endereco = new Endereco(dest.enderDest.xLgr, dest.enderDest.xBairro, dest.enderDest.xMun, dest.enderDest.nro,
                            dest.enderDest.CEP, dest.enderDest.UF, dest.enderDest.fone.ToString(), dest.enderDest.UF);
                }
                Destinatario = new Destinatario(dest.xNome,cpfcnpj, dest.IE, endereco);
            }


            #endregion

            #region Protocolo

            ProtocoloUso = protocolo;
            #endregion

            #region Produtos

            Produtos = new List<Produto>();
            nfe.infNFe.det.ForEach(item =>
            {
                var st = "";
                decimal valorIcms=0;
                decimal icmsBaseCalculo = 0;
                decimal pIcms = 0;
                if (item.imposto.ICMS != null)
                {
                     var cst = item.imposto.ICMS.TipoICMS.Cst();
                     var csosn = item.imposto.ICMS.TipoICMS.Csosn();
                     st = cst + csosn;
                     valorIcms = item.imposto.ICMS.TipoICMS.vICMS();
                     icmsBaseCalculo = item.imposto.ICMS.TipoICMS.vBC();
                     pIcms = item.imposto.ICMS.TipoICMS.pICMS();
                }

                Produtos.Add(new Produto(item.prod.cProd, item.prod.xProd, item.infAdProd,
                        item.prod.uTrib, item.prod.qTrib, item.prod.vUnTrib, valorIcms, item.prod.NCM, item.prod.CFOP.ToString(),
                        st, icmsBaseCalculo,pIcms,item.imposto?.IPI?.TipoIPI.pIPI(), item.imposto?.IPI?.TipoIPI.vIPI(),item.prod.vProd));
                 
            });

            #endregion

            #region Faturas

            Faturas = new List<Fatura>();
            nfe.infNFe.cobr?.dup?.ForEach(item =>
            {
                var data = item.dVenc ?? CrossCutting.Utils.ObterDataBrasil();
                Faturas.Add(new Fatura(item.nDup, data, item.vDup));
            });


            #endregion

            #region Status

            Producao = nfe.infNFe.ide.tpAmb == TipoAmbiente.Producao;
            Status = status;

            #endregion

            #region Inf Adicionais

            var infComplento = "";
            var infAdFisco = "";
           
            if (nfe.infNFe.infAdic != null)
            {
                infComplento = nfe.infNFe.infAdic.infCpl;
                infAdFisco = nfe.infNFe.infAdic.infAdFisco;
            }

            Inf = new InfAdic(infComplento, infAdFisco);
            #endregion

            #region Imposto

            Imposto = new Imposto(nfe.infNFe.total.ICMSTot.vBC, nfe.infNFe.total.ICMSTot.vICMS, nfe.infNFe.total.ICMSTot.vBCST,
                    nfe.infNFe.total.ICMSTot.vST, nfe.infNFe.total.ICMSTot.vProd, nfe.infNFe.total.ICMSTot.vFrete,
                    nfe.infNFe.total.ICMSTot.vSeg, nfe.infNFe.total.ICMSTot.vDesc, nfe.infNFe.total.ICMSTot.vOutro,
                    nfe.infNFe.total.ICMSTot.vIPI, nfe.infNFe.total.ICMSTot.vNF,nfe.infNFe.total.ICMSTot.vTotTrib,
                    nfe.infNFe.total.ICMSTot.vPIS, nfe.infNFe.total.ICMSTot.vCOFINS, nfe.infNFe.total.ICMSTot.vICMSUFRemet,
                    nfe.infNFe.total.ICMSTot.vICMSUFDest);
            

            
            #endregion

            Creditos = creditos;
            ISsqn = issqn;
            Chave = nfe.infNFe?.Id?.Replace("NFe", "");
            DataEmissao = nfe.infNFe.ide.dhEmi.ConverterFusoHorarioBrasil();  
            DataEntSaida = nfe.infNFe.ide.dhSaiEnt?.DateTime;
            NatOperacao = nfe.infNFe.ide.natOp;
            IeSt = nfe.infNFe.emit?.IEST;
            TipoOper = nfe.infNFe.ide.tpNF == TipoNFe.tnEntrada ? "0" : "1";
            ValorTotNota = Imposto.vNF;
            NumNota = nfe.infNFe.ide.nNF.ToString();
            SerieNota = nfe.infNFe.ide.serie.ToString();
            
        }


        #endregion
    }
}