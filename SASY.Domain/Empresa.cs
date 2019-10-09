using System;
using System.Collections.Generic;
using System.Text;

namespace SASY.Domain
{
    public class Empresa
    {
        public string Id { get; private set; }
        public string Nombre { get; private set; }
        public string Domicilio { get; private set; }
        public string Estado { get; private set; }
        public string Telefono { get; private set; }
        public string Correo { get; private set; }
        public ICollection<Persona> Personas { get; private set; }
        public ICollection<string> IdsEncuestasContratadas { get; private set; }

        internal Empresa() { }//Para EF
        public Empresa(string nombre, string estado, string domicilio, string telefono, string correo)
        {   
            Id = Guid.NewGuid().ToString();            
            Personas = new List<Persona>();
            IdsEncuestasContratadas = new HashSet<string>();
            AsignarDatos(nombre, estado, domicilio, telefono, correo);
        }

        public Persona AgregarPersona(string nombre, string apellidos, string correo, string genero, string puesto, string area, string estadoCivil, int antiguedad)
        {
            var persona = new Persona(nombre, apellidos, correo, genero, puesto, area, estadoCivil, antiguedad);
            Personas.Add(persona);
            return persona;
        }

        public void AgregarIdEncuestaContratada(string idEncuesta)
        {
            if (string.IsNullOrEmpty(idEncuesta))
                throw new ArgumentException(nameof(idEncuesta));
            if (IdsEncuestasContratadas.Contains(idEncuesta))
                throw new Exception($"Identificador de encuesta ya existe: {idEncuesta}");
            IdsEncuestasContratadas.Add(idEncuesta);
        }

        public void ModificarGenerales(string nombre, string estado, string domicilio, string telefono, string correo)
        {
            AsignarDatos(nombre, estado, domicilio, telefono, correo);
        }

        private void AsignarDatos(string nombre, string estado, string domicilio, string telefono, string correo)
        {
            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException(nameof(nombre));
            if (string.IsNullOrEmpty(correo))
                throw new ArgumentException(nameof(correo));
            if (string.IsNullOrEmpty(estado))
                throw new ArgumentException(nameof(estado));
            Nombre = nombre;
            Domicilio = domicilio;
            Telefono = telefono;
            Correo = correo;
            Estado = estado;
        }
    }
}
