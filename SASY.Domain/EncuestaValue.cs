using System;
using System.Collections.Generic;
using System.Text;

namespace SASY.Domain
{
    public class EncuestaValue
    {
        public string Nombre { get; private set; }
        public int MinutosParaResponder { get; private set; }
        internal EncuestaValue() { } //Para EF
        public EncuestaValue(string nombre, int minutosParaResponder)
        {
            Nombre = nombre;
            MinutosParaResponder = minutosParaResponder;
        }
    }
}
