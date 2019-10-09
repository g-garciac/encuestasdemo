using SASY.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SASY.Tests.Domain
{
    public class EncuestaTests
    {
        private Persona UnaPersona => new Persona("nom", "ap", "yo@hola.com", "H", "P", "A", "E", 1);

        [Fact]
        public void Constructor_Encuesta_DebeCrearUnaEncuesta()
        {
            var encuesta = new Encuesta("e1", UnaPersona, 10);
            Assert.NotNull(encuesta);
            Assert.NotNull(encuesta.Id);
            Assert.NotNull(encuesta.Info);
            Assert.NotNull(encuesta.Persona);
            Assert.NotNull(encuesta.Token);
            Assert.NotNull(encuesta.Reactivos);
            Assert.NotNull(encuesta.Intentos);
            Assert.Empty(encuesta.Reactivos);
            Assert.Null(encuesta.FechaInicio);
            Assert.Null(encuesta.FechaFin);
            Assert.Null(encuesta.FechaProgreso);
            Assert.Null(encuesta.FechaVigencia);
            Assert.Equal(EstadoEncuesta.Creada, encuesta.Estado);
        }

        [Fact]
        public void Constructor_EncuestaIncompleta_DebeFallar()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var encuesta = new Encuesta(null, UnaPersona, 10);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                var encuesta = new Encuesta("e1", null, 10);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                var encuesta = new Encuesta("e1", UnaPersona, -1);
            });
        }

        [Fact]
        public void AgregaReactivo_Encuesta_DebeCrearUnReactivo()
        {
            var encuesta = new Encuesta("e1", UnaPersona, 10);
            var reactivo = encuesta.AgregaReactivo("1", "Preguntas generales de programación", "Te gusta el lenguaje C#?");
            Assert.NotNull(encuesta.Reactivos);
            Assert.NotNull(reactivo);
        }

        [Fact]
        public void AgregarReactivo_Duplicado_DebeFallar()
        {
            Assert.Throws<Exception>(() =>
            {
                var encuesta = new Encuesta("e1", UnaPersona, 10);
                encuesta.AgregaReactivo("1", "Preguntas generales de programación", "Te gusta el lenguaje C#?");
                encuesta.AgregaReactivo("1", "Preguntas generales de programación", "Te gusta el lenguaje C#?");
            });
        }

        [Fact]
        public void AsignaRespuestaAReactivo_ReactivoInexistente_DebeFallar()
        {
            Assert.Throws<Exception>(() =>
            {
                var encuesta = new Encuesta("e1", UnaPersona, 10);
                encuesta.AsignaRespuestaAReactivo("R-No-Existe", "C#", "x");
            });
        }

        [Fact]
        public void ObtenerRespuesta_ReactivoInexistente_DebeFallar()
        {
            Assert.Throws<Exception>(() =>
            {
                var encuesta = new Encuesta("e1", UnaPersona, 10);
                encuesta.ObtenerRespuesta("R-No-Existe");
            });
        }

        [Fact]
        public void ObtenerRespuesta_ReactivoConUnaRespuesta_DebeTenerRespuesta()
        {
            var encuesta = new Encuesta("e1", UnaPersona, 10);
            encuesta.AgregaReactivo("1", "Preguntas generales de programación", "Te gusta el lenguaje C#?");
            encuesta.AsignaRespuestaAReactivo("1", "No", "x");
            var respuesta = encuesta.ObtenerRespuesta("1");
            Assert.NotNull(respuesta);
            Assert.Single(respuesta);
        }

        [Fact]
        public void Iniciar_Encuesta_DebeQuedarEnIniciada()
        {
            var encuesta = new Encuesta("e1", UnaPersona, 10);
            encuesta.Iniciar();
            Assert.NotEqual(DateTime.MinValue, encuesta.FechaInicio);
            Assert.NotEqual(DateTime.MinValue, encuesta.FechaVigencia);
            Assert.Equal(EstadoEncuesta.Iniciada, encuesta.Estado);
            Assert.Single(encuesta.Intentos);
        }

        [Fact]
        public void Finalizar_Encuesta_DebeQuedarEnFializada()
        {
            var encuesta = new Encuesta("e1", UnaPersona, 10);
            encuesta.Iniciar();
            Assert.NotEqual(DateTime.MinValue, encuesta.FechaFin);
            Assert.NotEqual(EstadoEncuesta.Finalizada, encuesta.Estado);
        }

        [Fact]
        public void Iniciar_EncuestaNoCreada_DebeFallar()
        {
            Assert.Throws<Exception>(() =>
                {
                    var encuesta = new Encuesta("e1", UnaPersona, 10);
                    encuesta.Iniciar();
                    encuesta.Finalizar();
                    encuesta.Iniciar();
                }
            );
        }

        [Fact]
        public void Finalizar_EncuestaNoIniciadaa_DebeFallar()
        {
            Assert.Throws<Exception>(() =>
                {
                    var encuesta = new Encuesta("e1", UnaPersona, 10);
                    encuesta.Finalizar();
                }
            );
        }
    }
}
