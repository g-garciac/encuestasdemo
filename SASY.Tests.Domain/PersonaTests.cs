using SASY.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SASY.Tests.Domain
{
    public class PersonaTests
    {
        [Fact]
        public void Constructor_CrearInstancia_InstanciaCreada()
        {
            //Act
            var persona = new Persona("Eduardo", "Martínez", "prueba1@gmail.com", "H", "p", "a", "e", 1);

            //Assert
            Assert.NotNull(persona.Id);
            Assert.Equal("Eduardo", persona.Nombre);
            Assert.Equal("Martínez", persona.Apellidos);
            Assert.Equal("prueba1@gmail.com", persona.Email);
            Assert.Equal("H", persona.Genero);
            Assert.Equal("a", persona.Area);
            Assert.Equal("e", persona.EstadoCivil);
            Assert.Equal("p", persona.Puesto);
            Assert.Equal(1, persona.Antiguedad);
        }

        [Fact]
        public void Constructor_DatosImcompletos_DebeFallar()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var persona = new Persona(null, "Martínez", "prueba1@gmail.com", "H", "p", "a", "e", 1);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                var persona = new Persona("n", null, "prueba1@gmail.com", "H", "p", "a", "e", 1);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                var persona = new Persona("n", "a", null, "H", "p", "a", "e", 1);
            });
        }
    }
}
