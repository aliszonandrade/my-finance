 using System;
using Xunit;
using MyFinance.Models;

namespace ProjetoTeste
{
    public class UnitTesteModel
    {
        [Fact]
        public void Test1()
        {
            UsuarioModel usuario = new UsuarioModel();
            usuario.Email = "alisson-andrade@outlook.com";
            usuario.Senha = "123456";
            Assert.True(usuario.ValidarLogin());

        }

        [Fact]
        public void TesteRegistrarUsuario()
        {
            UsuarioModel usuario = new UsuarioModel();
            usuario.Nome = "Teste";
            usuario.Email = "aloliveira@gmail.com";
            usuario.Senha = "123456";
            usuario.RegistrarUsuario();
            Assert.True(usuario.ValidarLogin());
        }


    }
}
