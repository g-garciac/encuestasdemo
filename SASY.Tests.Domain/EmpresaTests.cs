using SASY.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SASY.Tests.Domain
{
    public class EmpresaTests
    {
        [Fact]
        public void Empresa_CrearInstancia_InstnciaCreada()
        {
            //Act
            var empresa = new Empresa("patito", "edo", "d", "t", "c");
            //Assert:
            Assert.NotNull(empresa);
            Assert.NotNull(empresa.Id);
            Assert.Equal("patito", empresa.Nombre);
            Assert.NotNull(empresa.Personas);
            Assert.NotNull(empresa.IdsEncuestasContratadas);
            Assert.Equal(0, empresa.Personas.Count);
            Assert.Equal("d", empresa.Domicilio);
            Assert.Equal("t", empresa.Telefono);
            Assert.Equal("c", empresa.Correo);
            Assert.Equal("edo", empresa.Estado);
        }

        [Fact]
        public void AgregarPersona_CreacionNuevaPersona_EmpresaTieneAhoraUnaPersona()
        {
            //Arrange:
            var empresa = new Empresa("Patito", "edo", "d", "t", "c");

            //Act:
            var nuevaPersona = empresa.AgregarPersona("x", "xa", "hola@hola.com", "H", "P", "A", "E", 1);

            //Assert:
            Assert.Equal(1, empresa.Personas.Count);
        }

        [Fact]
        public void Empresa_CrearEmpresaSinNombre_DispareExcepcion()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var empresa = new Empresa("Patito", "edo", "d", "t", null);
            });
            Assert.Throws<ArgumentException>(() =>
            {
                var empresa = new Empresa(null, "edo", "d", "t", "c");
            });
            Assert.Throws<ArgumentException>(() =>
            {
                var empresa = new Empresa("nom", null, "d", "t", "c");
            });
        }

        [Fact]
        public void AgregarIdEncuestaContratada_SeAgreganVariosIds_DebenAgregarse()
        {
            var empresa = new Empresa("patito", "Tlaxcala", "Calle 1", "123456789", "a@a.com");
            empresa.AgregarIdEncuestaContratada("E-1");
            Assert.Single(empresa.IdsEncuestasContratadas);
        }
        [Fact]
        public void AgregarIdEncuestaContratada_IdsNuloOVacio_Debefallar()
        {
            var empresa = new Empresa("patito", "Tlaxcala", "Calle 1", "123456789", "a@a.com");
            Assert.Throws<ArgumentException>(() =>
            {
                empresa.AgregarIdEncuestaContratada("");
            });
        }
        [Fact]
        public void AgregarIdEncuestaContratada_IdsDuplicado_Debefallar()
        {
            var empresa = new Empresa("patito", "Tlaxcala", "Calle 1", "123456789", "a@a.com");
            empresa.AgregarIdEncuestaContratada("E-1");
            Assert.Throws<Exception>(() =>
            {
                empresa.AgregarIdEncuestaContratada("E-1");
            });
        }

        [Fact]
        public void Empresa_Modificar_DebeAsignarNuevosDatos()
        {
            var empresa = new Empresa("patito", "edo", "d", "t", "c@a.com");
            empresa.ModificarGenerales("gansito", "tlx", "calle1", "1234567890", "nuevo@hotmail.com");
            var id = new string(empresa.Id);
            Assert.Equal("gansito", empresa.Nombre);
            Assert.Equal(0, empresa.Personas.Count);
            Assert.Equal("calle1", empresa.Domicilio);
            Assert.Equal("1234567890", empresa.Telefono);
            Assert.Equal("nuevo@hotmail.com", empresa.Correo);
            Assert.Equal("tlx", empresa.Estado);
            Assert.Equal(id, empresa.Id);
            Assert.NotNull(empresa.Personas);
            Assert.NotNull(empresa.IdsEncuestasContratadas);
        }
    }
}
