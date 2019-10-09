using SASY.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SASY.Domain
{
    public enum EstadoEncuesta
    {
        Creada,
        Iniciada,
        Finalizada,
    }
    public class Encuesta
    {
        public string Id { get; private set; }
        public EstadoEncuesta Estado { get; set; }
        public EncuestaValue Info { get; private set; }
        public Persona Persona { get; private set; }
        public DateTime? FechaInicio { get; private set; }
        public DateTime? FechaFin { get; private set; }
        public DateTime? FechaProgreso { get; private set; }
        public DateTime? FechaVigencia { get; private set; }
        public string Token { get; private set; }
        public ICollection<ReactivoEncuesta> Reactivos { get; private set; }
        public ICollection<DateTime> Intentos { get; private set; }

        internal Encuesta() { }//Para EF
        public Encuesta(string nombre, Persona persona, int minutosVigencia)
        {
            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException(nameof(nombre));
            if (persona is null)
                throw new ArgumentException(nameof(persona));
            if (minutosVigencia <= 0)
                throw new ArgumentException(nameof(minutosVigencia));
            Id = Guid.NewGuid().ToString();
            Info = new EncuestaValue(nombre, minutosVigencia);
            Persona = persona;
            Token = CrearTokenEncuesta();
            Reactivos = new List<ReactivoEncuesta>();
            Intentos = new List<DateTime>();
            Estado = EstadoEncuesta.Creada;
        }

        public void Iniciar()
        {
            FechaInicio = DateTime.UtcNow;
            if (Estado != EstadoEncuesta.Creada)
                throw new Exception($"No se puede iniciar Encuesta {Estado}");
            FechaVigencia = FechaInicio.Value.AddMinutes(Info.MinutosParaResponder);
            Estado = EstadoEncuesta.Iniciada;
            Intentos.Add(FechaInicio.Value);
        }

        public void Finalizar()
        {
            if (Estado == EstadoEncuesta.Creada)
                throw new Exception($"No se puede finalizar Encuesta {Estado}");
            FechaFin = DateTime.UtcNow;
            Estado = EstadoEncuesta.Finalizada;
        }

        private string CrearTokenEncuesta()
        {
            return $"{Persona.Id.Replace("-", "")}{Guid.NewGuid().ToString().Replace("-", "")}";
        }

        public ReactivoEncuesta AgregaReactivo(string idReactivo, string clasificacion, string descripcion)
        {
            var reactivo = Reactivos.FirstOrDefault(r => r.Id.Equals(idReactivo));
            if (!(reactivo is null))
            {
                throw new Exception($"Ya existe el reactivo {idReactivo}");
            }
            reactivo = new ReactivoEncuesta(idReactivo, clasificacion, descripcion);
            Reactivos.Add(reactivo);
            return reactivo;
        }

        public void AsignaRespuestaAReactivo(string idReactivo, string claveRespuesta, string valorRespuesta)
        {
            var reactivo = Reactivos.FirstOrDefault(r => r.Id.Equals(idReactivo));
            if (reactivo is null)
            {
                throw new Exception($"No existe el Reactivo {idReactivo}");
            }
            reactivo.AsignaRespuesta(claveRespuesta, valorRespuesta);
        }

        public Dictionary<string, string> ObtenerRespuesta(string idReactivo)
        {
            var reactivo = Reactivos.FirstOrDefault(r => r.Id.Equals(idReactivo));
            if (reactivo is null)
            {
                throw new Exception($"No existe el Reactivo {idReactivo}");
            }
            return reactivo.Respuesta;
        }
    }
}