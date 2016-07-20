using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using NFe.Classes;
using NFe.Classes.Servicos.Autorizacao;

namespace NFe.Integracao
{
    /// <summary>
    /// Classe responsável por desserializar os arquivos xml em classes do Zeus.
    /// Inicialmente a única fonte de dados suportada são arquivos xml. Futuramente pode-se extender a classe para suportar outras fontes.
    /// </summary>
    public class NFeBuilder
    {
        IList<Classes.NFe> NFe;

        public NFeBuilder(string pathArquivoXml, TipoXmlNFe tipo)
        {
            this.NFe = new List<Classes.NFe>();

            try
            {
                StringReader reader = new StringReader(File.ReadAllText(pathArquivoXml));
                if (tipo == TipoXmlNFe.Destinatario)
                {
                    XmlSerializer desserializador = new XmlSerializer(typeof(nfeProc));
                    this.NFe.Add(((nfeProc)desserializador.Deserialize(reader)).NFe);
                }
                else if(tipo == TipoXmlNFe.Lote)
                {
                    XmlSerializer desserializador = new XmlSerializer(typeof(enviNFe3));
                    this.NFe = ((enviNFe3)desserializador.Deserialize(reader)).NFe;
                }
                else
                {
                    XmlSerializer desserializador = new XmlSerializer(typeof(Classes.NFe));
                    this.NFe.Add(((Classes.NFe)desserializador.Deserialize(reader)));
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
            if (this.NFe.Count > 0)
            {
                Classes.NFe nfe = this.NFe[this.NFe.Count - 1];
                this.NFe.Remove(nfe);
                return nfe;
            }
            else
            {
                throw new InvalidOperationException("Não existem informações para a montagem da classe");
            }
        }

        /// <summary>
        /// Monta uma lista de NF-e.
        /// </summary>
        /// <returns>Retorna todas as NF-e´s presentes em "this.NFe"</returns>
        public IList<Classes.NFe> BuildAll()
        {
            if (this.NFe.Count > 0)
            {
                return this.NFe;
            }
            else
            {
                throw new InvalidOperationException("Não existem informações para a montagem da classe");
            }
        }
    }
}
