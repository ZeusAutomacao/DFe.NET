using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Recebedor
{
    public class receb
    {
        public enderReceb enderReceb;

        private string _CNPJ;
        private string _CPF;
        private string _IE;
        private string _xNome;
        private string _fone;
        private string _email;

        public string CNPJ { get { return _CNPJ; } set { _CNPJ = value; } }
        public string CPF { get { return _CPF; } set { _CPF = value; } }
        public string IE { get { return _IE; } set { _IE = value; } }
        public string xNome { get { return _xNome; } set { _xNome = value; } }
        public string fone { get { return _fone; } set { _fone = value; } }
        public string email { get { return _email; } set { _email = value; } }
    }
}
