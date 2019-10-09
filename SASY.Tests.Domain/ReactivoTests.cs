using SASY.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SASY.Tests.Domain
{
    public class ReactivoTests
    {
        [Fact]
        public void Constructor_Reactivo_DebeCrearUnReactivo()
        {
            var reactivo = new ReactivoEncuesta("1", "Preguntas generales de programación", "Te gusta el lenguaje C#?");
            Assert.Equal("Preguntas generales de programación", reactivo.Clasificacion);
            Assert.Equal("Te gusta el lenguaje C#?", reactivo.Descripcion);
            Assert.Null(reactivo.FechaAsignacionRespuesta);
            Assert.NotNull(reactivo.Respuesta);
        }

        [Fact]
        public void AsignaRespuesta_PrimerAsignacion_DebeCrearUnaRespuesta()
        {
            var reactivo = new ReactivoEncuesta("1", "Preguntas generales de programación", "Te gusta el lenguaje C#?");
            reactivo.AsignaRespuesta("Sí", "x");
            Assert.Single(reactivo.Respuesta);
            Assert.Equal("x", reactivo.Respuesta["Sí"]);
            Assert.NotEqual(DateTime.MinValue, reactivo.FechaAsignacionRespuesta);
        }

        [Fact]
        public void AsignaRespuesta_SegundaAsignacionAMismoReactiv_NoDebeCrearUnaRespuesta()
        {
            var reactivo = new ReactivoEncuesta("1", "Preguntas generales de programación", "Te gusta el lenguaje C#?");
            reactivo.AsignaRespuesta("Sí", "x");
            reactivo.AsignaRespuesta("Sí", "");
            Assert.Equal("", reactivo.Respuesta["Sí"]);
        }

        [Fact]
        public void TieneRespuesta_UnaMismaAsignacion_DebeTenerUnaSolaRespuesta()
        {
            var reactivo = new ReactivoEncuesta("1", "Preguntas generales de programación", "Te gusta el lenguaje C#?");
            reactivo.AsignaRespuesta("Sí", "x");
            var tieneRespuesta = reactivo.TieneRespuesta();
            Assert.True(tieneRespuesta);
        }

        [Fact]
        public void ObtenerRespuesta_ReactivoConDosRespuestas_DebeRegresarDosRespuestas()
        {
            var reactivo = new ReactivoEncuesta("1", "Preguntas generales de programación", "Qué lenguajes conoces?");
            reactivo.AsignaRespuesta("C#", "x");
            reactivo.AsignaRespuesta("Java", "x");
            var respuestas = reactivo.Respuesta;
            Assert.Equal(2, respuestas.Count);
            Assert.Equal("x", respuestas["C#"]);
            Assert.Equal("x", respuestas["Java"]);
        }
    }
}
