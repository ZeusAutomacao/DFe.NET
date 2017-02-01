using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeComplementar
{
    public class infCteComp
    {
        public vPresComp vPresComp;
        public impComp impComp;

        private string _chave;

        public string chave { get { return _chave; } set { _chave = value; } }
    }
}
