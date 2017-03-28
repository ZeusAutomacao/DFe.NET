using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.w3.org/2000/09/xmldsig#", ElementName = "Signature")]
    public class FusionAssinaturaDigital
    {
        [XmlElement(ElementName = "SignedInfo")]
        public FusionInformacoesAssinatura InformacoesAssinatura { get; set; }

        [XmlElement(ElementName = "SignatureValue")]
        public string SignatureValue { get; set; }

        [XmlElement(ElementName = "KeyInfo")]
        public FusionKeyInfo KeyInfo { get; set; }
    }

    [Serializable]
    public class FusionKeyInfo
    {
        [XmlElement(ElementName = "X509Data")]
        public FusionX509Data X509Data { get; set; }

        public FusionKeyInfo()
        {
            X509Data = new FusionX509Data();
        }
    }

    [Serializable]
    public class FusionX509Data
    {
        [XmlElement(ElementName = "X509Certificate")]
        public string X509Certificate { get; set; }
    }

    [Serializable]
    public class FusionInformacoesAssinatura
    {
        [XmlElement(ElementName = "CanonicalizationMethod")]
        public FusionCanonicalizationMethod CanonicalizationMethod { get; set; }

        [XmlElement(ElementName = "SignatureMethod")]
        public FusionSignatureMethod SignatureMethod { get; set; }

        [XmlElement(ElementName = "Reference")]
        public FusionReference Reference { get; set; }
    }

    [Serializable]
    public class FusionCanonicalizationMethod
    {
        [XmlAttribute]
        public string Algorithm { get; set; }
    }

    [Serializable]
    public class FusionSignatureMethod
    {
        [XmlAttribute]
        public string Algorithm { get; set; }
    }

    [Serializable]
    public class FusionReference
    {
        [XmlAttribute(AttributeName = "URI")]
        public string Uri { get; set; }

        [XmlArray(ElementName = "Transforms"), XmlArrayItem(typeof (FusionTransform), ElementName = "Transform")]
        public List<FusionTransform> Transforms { get; set; }

        [XmlElement(ElementName = "DigestMethod")]
        public FusionDigestMethod DigestMethod { get; set; }

        [XmlElement(ElementName = "DigestValue")]
        public string DigestValue { get; set; }

        public FusionReference()
        {
            Transforms = new List<FusionTransform>();
        }
    }

    [Serializable]
    [XmlRoot(ElementName = "Transform")]
    public class FusionTransform
    {
        [XmlAttribute]
        public string Algorithm { get; set; }
    }

    [Serializable]
    public class FusionDigestMethod
    {
        [XmlAttribute]
        public string Algorithm { get; set; }
    }
}