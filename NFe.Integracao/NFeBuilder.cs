using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using NFe.Classes;
using NFe.Classes.Servicos.Autorizacao;
using NFe.Integracao.Enums;

namespace NFe.Integracao
{
    /// <summary>
    /// Classe responsável por desserializar os arquivos xml em classes do Zeus.
    /// Inicialmente a única fonte de dados suportada são arquivos xml. Futuramente pode-se extender a classe para suportar outras fontes.
    /// </summary>
    public class NFeBuilder
    {
        private readonly IList<Classes.NFe> _nFe;

        public NFeBuilder(string pathArquivoXml, TipoXmlNFe tipo)
        {
            _nFe = new List<Classes.NFe>();

            try
            {
                var reader = new StringReader(File.ReadAllText(pathArquivoXml));
                switch (tipo)
                {
                    case TipoXmlNFe.Destinatario:
                    {
                        var desserializador = new XmlSerializer(typeof(nfeProc));
                        _nFe.Add(((nfeProc)desserializador.Deserialize(reader)).NFe);
                    }
                        break;
                    case TipoXmlNFe.Lote:
                    {
                        var desserializador = new XmlSerializer(typeof(enviNFe3));
                        _nFe = ((enviNFe3)desserializador.Deserialize(reader)).NFe;
                    }
                        break;
                    default:
                    {
                        var desserializador = new XmlSerializer(typeof(Classes.NFe));
                        _nFe.Add(((Classes.NFe)desserializador.Deserialize(reader)));
                    }
                        break;
                }
            }
            catch(Exception ex)
            {
                throw new InvalidDataException("Arquivo XML inválido.", ex);
            }
        }

        /// <summary>
        /// Método responsável por montar uma NF-e.
        /// </summary>
        /// <returns>Retorna uma das notas presentes em "this.NFe" removendo a mesma da listagem.</returns>
        public Classes.NFe Build()
        {
            if (_nFe.Count <= 0)
                throw new InvalidOperationException("Não existem informações para a montagem da classe");
            var nfe = _nFe[_nFe.Count - 1];
            _nFe.Remove(nfe);
            return nfe;
        }

        /// <summary>
        /// Monta uma lista de NF-e.
        /// </summary>
        /// <returns>Retorna todas as NF-e´s presentes em "this.NFe"</returns>
        public IList<Classes.NFe> BuildAll()
        {
            if (_nFe.Count <= 0)
                throw new InvalidOperationException("Não existem informações para a montagem da classe");
            return _nFe;
        }
    }
}