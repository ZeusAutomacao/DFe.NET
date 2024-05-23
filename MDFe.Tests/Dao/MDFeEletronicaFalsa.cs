using System;
using System.Collections.Generic;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using MDFe.Classes.Flags;
using MDFe.Classes.Informacoes;
using MDFe.Tests.Entidades;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Tests.Dao
{
    public class MDFeEletronicaFalsa
    {
        private MDFeEletronico _mdfe;
        private Empresa _empresa;

        public MDFeEletronicaFalsa(Empresa empresa)
        {
            _empresa = empresa;
            _mdfe = new MDFeEletronico();
            Ide();
            Emitente();
            Modal();
            InfMunDescarga();
            Seg();
            Tot();
            MDFeInfAdic();
        }

        private void Ide()
        {
            _mdfe.InfMDFe.Ide.CUF = Estado.SE;
            _mdfe.InfMDFe.Id = "00000000000000000000000000000000000000000000";
            _mdfe.InfMDFe.Ide.TpAmb = TipoAmbiente.Homologacao;
            _mdfe.InfMDFe.Ide.TpEmit = MDFeTipoEmitente.PrestadorServicoDeTransporte;
            _mdfe.InfMDFe.Ide.Mod = ModeloDocumento.MDFe;
            _mdfe.InfMDFe.Ide.Serie = 750;
            _mdfe.InfMDFe.Ide.NMDF = 6;
            _mdfe.InfMDFe.Ide.CMDF = 00000000;
            _mdfe.InfMDFe.Ide.Modal = MDFeModal.Rodoviario;
            _mdfe.InfMDFe.Ide.DhEmi = new DateTime(2018,10,22,11,06,22,03);
            _mdfe.InfMDFe.Ide.TpEmis = MDFeTipoEmissao.Normal;
            _mdfe.InfMDFe.Ide.ProcEmi = MDFeIdentificacaoProcessoEmissao.EmissaoComAplicativoContribuinte;
            _mdfe.InfMDFe.Ide.VerProc = "versao28383";
            _mdfe.InfMDFe.Ide.UFIni = Estado.SE;
            _mdfe.InfMDFe.Ide.UFFim = Estado.SE;
            _mdfe.InfMDFe.Versao = VersaoServico.Versao300;
            

            _mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "0000000",
                XMunCarrega = "INEXISTENTE"
            });

            _mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "0000000",
                XMunCarrega = "INEXISTENTE"
            });

            _mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "0000000",
                XMunCarrega = "INEXISTENTE"
            });
        }

        private void Emitente()
        {
            _mdfe.InfMDFe.Emit.CNPJ = _empresa.Cnpj;
            _mdfe.InfMDFe.Emit.IE = _empresa.InscricaoEstadual;
            _mdfe.InfMDFe.Emit.XNome = _empresa.Nome;
            _mdfe.InfMDFe.Emit.XFant = _empresa.NomeFantasia;

            _mdfe.InfMDFe.Emit.EnderEmit.XLgr = _empresa.Logradouro;
            _mdfe.InfMDFe.Emit.EnderEmit.Nro = _empresa.Numero;
            _mdfe.InfMDFe.Emit.EnderEmit.XCpl = _empresa.Complemento;
            _mdfe.InfMDFe.Emit.EnderEmit.XBairro = _empresa.Bairro;
            _mdfe.InfMDFe.Emit.EnderEmit.CMun = _empresa.CodigoIbgeMunicipio;
            _mdfe.InfMDFe.Emit.EnderEmit.XMun = _empresa.NomeMunicipio;
            _mdfe.InfMDFe.Emit.EnderEmit.CEP = long.Parse(_empresa.Cep);
            _mdfe.InfMDFe.Emit.EnderEmit.UF = _empresa.SiglaUf;
            _mdfe.InfMDFe.Emit.EnderEmit.Fone = _empresa.Telefone;
            _mdfe.InfMDFe.Emit.EnderEmit.Email = _empresa.Email;
        }

        private void Modal()
        {
            _mdfe.InfMDFe.InfModal.VersaoModal = MDFeVersaoModal.Versao300;
            _mdfe.InfMDFe.InfModal.Modal = new MDFeRodo
            {
                infANTT = new MDFeInfANTT
                {

                    // não é obrigatorio
                    infCIOT = new List<infCIOT>
                        {
                            new infCIOT
                            {
                                CIOT = "000000000000",
                                CNPJ = "00000000000000"
                            }
                        },
                    valePed = new MDFeValePed
                    {
                        Disp = new List<MDFeDisp>
                                    {
                                        new MDFeDisp
                                        {
                                            CNPJForn = "00000000000000",
                                            CNPJPg = "00000000000000",
                                            NCompra = "000000000",
                                            vValePed = 100.00m
                                        }
                                    }
                    }
                },

                VeicTracao = new MDFeVeicTracao
                {
                    Placa = "AAA0000",
                    RENAVAM = "000000000",
                    UF = Estado.SE,
                    Tara = 000,
                    CapM3 = 000,
                    CapKG = 00,
                    Condutor = new List<MDFeCondutor>
                        {
                            new MDFeCondutor
                            {
                                CPF = "00000000000",
                                XNome = "NINGUEM"
                            }
                        },
                    TpRod = MDFeTpRod.Outros,
                    TpCar = MDFeTpCar.NaoAplicavel
                },

                lacRodo = new List<MDFeLacre>
                    {
                        new MDFeLacre
                        {
                            NLacre = "lacre01"
                        }
                    }

            };
        }

        private void InfMunDescarga()
        {
            _mdfe.InfMDFe.InfDoc.InfMunDescarga = new List<MDFeInfMunDescarga>
            {
                new MDFeInfMunDescarga
                {
                    XMunDescarga = "CUIABA",
                    CMunDescarga = "5103403",
                    InfCTe = new List<MDFeInfCTe>
                    {
                        new MDFeInfCTe
                        {
                            ChCTe = "00000000000000000000000000000000000000000000"
                        }
                    }
                }
            };

            _mdfe.InfMDFe.InfDoc.InfMunDescarga[0].InfCTe[0].Peri = new List<MDFePeri>
            {
                new MDFePeri
                {
                    NONU = "0000",
                    QTotProd = "quantidade 20"
                }
            };

        }

        private void Seg()
        {
            _mdfe.InfMDFe.Seg = new List<MDFeSeg>();

            _mdfe.InfMDFe.Seg.Add(new MDFeSeg
            {
                InfResp = new MDFeInfResp
                {
                    CNPJ = "00000000000000",
                    RespSeg = MDFeRespSeg.EmitenteDoMDFe
                },
                InfSeg = new MDFeInfSeg
                {
                    CNPJ = "00000000000000",
                    XSeg = "TESTE"
                },
                NApol = "TESTE",
                NAver = new List<string>
                {
                    "TESTE"
                }
            });
        }

        private void Tot()
        {
            _mdfe.InfMDFe.Tot.QCTe = 1;
            _mdfe.InfMDFe.Tot.vCarga = 500.00m;
            _mdfe.InfMDFe.Tot.CUnid = MDFeCUnid.KG;
            _mdfe.InfMDFe.Tot.QCarga = 100.0000m;
        }

        private void MDFeInfAdic()
        {
            _mdfe.InfMDFe.InfAdic = new MDFeInfAdic
            {
                InfCpl = "TESTE"
            };

        }

        public MDFeEletronico GetMdfe()
        {
            return _mdfe;
        }
    }
}
