using System;

namespace DFe.Http.SoapAtributos
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DFeSoapAction : Attribute
    {
        public string Acao { get; set; }
    }
}