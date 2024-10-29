using System.Xml.Serialization;

namespace NFe.Classes.Servicos.Download
{   
    public class retNFe
    {
        /// <summary>
        /// JR09 - Chave de acesso da NF-e 
        /// </summary>
        public string chNFe { get; set; }

        /// <summary>
        /// JR10 - Código do status da resposta
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        /// JR11 - Descrição literal do status da resposta
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        /// JR13 - Estrutura “procNFe”, compactado no padrão gZip, o tipo do campo é base64Binary. 
        /// JR14 - Estrutura “procNFe”, descompactada
        /// JR17 - Grupo contendo a NF-e compactada e o Protocolo de Autorização compactado
        /// </summary>        
        [XmlElementAttribute("procNFe", typeof(procNFe))]
        [XmlElementAttribute("procNFeGrupoZip", typeof(procNFeGrupoZip))]
        [XmlElementAttribute("procNFeZip", typeof(byte[]), DataType = "base64Binary")]
        public object XmlNfe { get; set; }

    }
}