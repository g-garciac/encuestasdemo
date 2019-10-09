using System;
using System.Collections.Generic;
using System.Text;

namespace SASY.Domain
{
    public class Persona
    {
        public string Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellidos { get; private set; }
        public string Email { get; private set; }
        public string Genero { get; private set; }
        public string Puesto { get; private set; }
        public string Area { get; private set; }
        public string EstadoCivil { get; private set; }
        public int Antiguedad { get; private set; }

        internal Persona() { }//Para EF
        public Persona(string nombre, string apellidos, string email, string genero, string puesto, string area, string estadoCivil, int antiguedad)
        {
            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException(nameof(nombre));
            if (string.IsNullOrEmpty(apellidos))
                throw new ArgumentException(nameof(apellidos));
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException(nameof(email));
            Id = Guid.NewGuid().ToString();
            Nombre = nombre;
            Apellidos = apellidos;
            Email = email;
            Genero = genero;
            Puesto = puesto;
            Area = area;
            EstadoCivil = estadoCivil;
            Antiguedad = antiguedad;
        }

    }
}
