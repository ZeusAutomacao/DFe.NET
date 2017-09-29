using System;

namespace DFe.Http.SoapAtributos
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DFeSoapAtributo : Attribute
    {
        public string Namespaces { get; set; }

        public DFeSoapAtributo(string namespaces)
        {
            Namespaces = namespaces;
        }
    }
}