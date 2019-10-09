using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SASY.Domain
{
    public class ReactivoEncuesta
    {
        public string Id { get; private set; }
        public string Clasificacion { get; private set; }
        public string Descripcion { get; private set; }
        public DateTime? FechaAsignacionRespuesta { get; private set; }
        public Dictionary<string, string> Respuesta { get; private set; }

        internal ReactivoEncuesta() { }//Para EF

        public ReactivoEncuesta(string id, string clasificacion, string descripcion)
        {
            Id = id;
            Clasificacion = clasificacion;
            Descripcion = descripcion;
            Respuesta = new Dictionary<string, string>();
        }

        public void AsignaRespuesta(string claveRespuesta, string valorRespuesta)
        {
            if (!Respuesta.ContainsKey(claveRespuesta))
            {
                Respuesta.Add(claveRespuesta, valorRespuesta);
            }
            else
            {
                Respuesta[claveRespuesta] = valorRespuesta;
            }
            FechaAsignacionRespuesta = DateTime.UtcNow;
        }

        public bool TieneRespuesta()
        {
            return Respuesta.Count > 0;
        }
        public Dictionary<string, string> ObtenerRespuesta()
        {
            if (!TieneRespuesta())
                throw new Exception($"El reactivo no tiene respuesta");
            return Respuesta;
        }

    }
}
