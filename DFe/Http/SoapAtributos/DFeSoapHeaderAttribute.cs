using System;

namespace DFe.Http.SoapAtributos
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DFeSoapHeaderAttribute : Attribute
    {
        public string HeaderName { get; set; }

        public string Namespace { get; set; }

        public DFeSoapHeaderAttribute(string headerName)
        {
            HeaderName = headerName;
        }
    }
}