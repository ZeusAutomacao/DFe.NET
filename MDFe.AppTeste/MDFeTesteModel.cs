using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;
using MDFe.AppTeste.ModelBase;

namespace MDFe.AppTeste
{
    public class MDFeTesteModel : ViewModel
    {
        private string _cnpj;
        private string _inscricaoEstadual;
        private string _nome;
        private string _nomeFantasia;
        private string _logradouro;
        private string _numero;
        private string _complemento;
        private string _bairro;
        private long _codigoIbgeMunicipio;
        private string _nomeMunicipio;
        private string _cep;
        private EstadoUF _siglaUf;
        private string _telefone;
        private string _email;
        private string _numeroDeSerie;
        private string _caminhoArquivo;
        private string _senha;
        private EstadoUF _ufDestino;
        private TipoAmbiente _ambiente;
        private short _serie;
        private long _numeracao;
        private VersaoServico _versaoMdFeRecepcao;
        private VersaoServico _versaoMdFeRetRecepcao;
        private VersaoServico _versaoMdFeRecepcaoEvento;
        private VersaoServico _versaoMdFeConsulta;
        private VersaoServico _versaoMdFeStatusServico;
        private VersaoServico _versaoMdFeConsNaoEnc;

        #region empresa

        public string Cnpj
        {
            get { return _cnpj; }
            set
            {
                _cnpj = value;
                OnPropertyChanged("Cnpj");
            }
        }

        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set
            {
                _inscricaoEstadual = value;
                OnPropertyChanged("InscricaoEstadual");
            }
        }

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged("Nome");
            }
        }

        public string NomeFantasia
        {
            get { return _nomeFantasia; }
            set
            {
                _nomeFantasia = value;
                OnPropertyChanged("NomeFantasia");
            }
        }

        public string Logradouro
        {
            get { return _logradouro; }
            set
            {
                _logradouro = value;
                OnPropertyChanged("Logradouro");
            }
        }

        public string Numero
        {
            get { return _numero; }
            set
            {
                _numero = value;
                OnPropertyChanged("Numero");
            }
        }

        public string Complemento
        {
            get { return _complemento; }
            set
            {
                _complemento = value;
                OnPropertyChanged("Complemento");
            }
        }

        public string Bairro
        {
            get { return _bairro; }
            set
            {
                _bairro = value;
                OnPropertyChanged("Bairro");
            }
        }

        public long CodigoIbgeMunicipio
        {
            get { return _codigoIbgeMunicipio; }
            set
            {
                _codigoIbgeMunicipio = value;
                OnPropertyChanged("CodigoIbgeMunicipio");
            }
        }

        public string NomeMunicipio
        {
            get { return _nomeMunicipio; }
            set
            {
                _nomeMunicipio = value;
                OnPropertyChanged("NomeMunicipio");
            }
        }

        public string Cep
        {
            get { return _cep; }
            set
            {
                _cep = value;
                OnPropertyChanged("Cep");
            }
        }

        public EstadoUF SiglaUf
        {
            get { return _siglaUf; }
            set
            {
                _siglaUf = value;
                OnPropertyChanged("SiglaUf");
            }
        }

        public string Telefone
        {
            get { return _telefone; }
            set
            {
                _telefone = value;
                OnPropertyChanged("Telefone");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        #endregion

        #region certificado

        public string NumeroDeSerie
        {
            get { return _numeroDeSerie; }
            set
            {
                _numeroDeSerie = value;
                OnPropertyChanged("NumeroDeSerie");
            }
        }

        public string CaminhoArquivo
        {
            get { return _caminhoArquivo; }
            set
            {
                _caminhoArquivo = value;
                OnPropertyChanged("CaminhoArquivo");
            }
        }

        public string Senha
        {
            get { return _senha; }
            set
            {
                _senha = value;
                OnPropertyChanged("Senha");
            }
        }

        #endregion

        #region configWebService

        public EstadoUF UfDestino
        {
            get { return _ufDestino; }
            set
            {
                _ufDestino = value;
                OnPropertyChanged("UfDestino");
            }
        }

        public TipoAmbiente Ambiente
        {
            get { return _ambiente; }
            set
            {
                _ambiente = value;
                OnPropertyChanged("Ambiente");
            }
        }

        public short Serie
        {
            get { return _serie; }
            set
            {
                _serie = value;
                OnPropertyChanged("Serie");
            }
        }

        public long Numeracao
        {
            get { return _numeracao; }
            set
            {
                _numeracao = value;
                OnPropertyChanged("Numeracao");
            }
        }

        public VersaoServico VersaoMDFeRecepcao
        {
            get { return _versaoMdFeRecepcao; }
            set
            {
                _versaoMdFeRecepcao = value;
                OnPropertyChanged("VersaoMDFeRecepcao");
            }
        }

        public VersaoServico VersaoMDFeRetRecepcao
        {
            get { return _versaoMdFeRetRecepcao; }
            set
            {
                _versaoMdFeRetRecepcao = value;
                OnPropertyChanged("VersaoMDFeRetRecepcao");
            }
        }

        public VersaoServico VersaoMDFeRecepcaoEvento
        {
            get { return _versaoMdFeRecepcaoEvento; }
            set
            {
                _versaoMdFeRecepcaoEvento = value;
                OnPropertyChanged("VersaoMDFeRecepcaoEvento");
            }
        }

        public VersaoServico VersaoMDFeConsulta
        {
            get { return _versaoMdFeConsulta; }
            set
            {
                _versaoMdFeConsulta = value;
                OnPropertyChanged("VersaoMDFeConsulta");
            }
        }

        public VersaoServico VersaoMDFeStatusServico
        {
            get { return _versaoMdFeStatusServico; }
            set
            {
                _versaoMdFeStatusServico = value;
                OnPropertyChanged("VersaoMDFeStatusServico");
            }
        }

        public VersaoServico VersaoMDFeConsNaoEnc
        {
            get { return _versaoMdFeConsNaoEnc; }
            set
            {
                _versaoMdFeConsNaoEnc = value;
                OnPropertyChanged("VersaoMDFeConsNaoEnc");
            }
        }

        #endregion


    }
}