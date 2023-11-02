using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class VeiculoTestes : IDisposable
    {
        private Veiculo veiculo;
        private ITestOutputHelper SaidaConsoleTeste;

        public VeiculoTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            veiculo = new Veiculo();
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado.");
        }

        [Fact]
        public void TestaVeiculoAcelerarComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Acelerar(10);

            //Assert
            Assert.Equal(100, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaVeiculoFrearComParametro10()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Act
            veiculo.Frear(10);

            //Assert
            Assert.Equal(-150, veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestaVeiculoTipoVeiculoPadraoAutomovel()
        {
            //Arrange
            //var veiculo = new Veiculo();

            //Asset
            Assert.Equal(TipoVeiculo.Automovel, veiculo.Tipo);
        }

        [Fact]
        public void TestaNomeProprietarioDoVeiculo()
        {
            // Arrange

            //Act

            //Assert
        }

        [Fact]
        public void TestaImprimirFichaDeInformacaoVeiculo()
        {
            //Arrange
            //var veiculo = new Veiculo();
            veiculo.Proprietario = "Everton Crispim";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Placa = "ABX-1234";
            veiculo.Cor = "Cinza";
            veiculo.Modelo = "Gol New City";

            //Act
            string dados = veiculo.ToString();

            //Assert
            Assert.Contains("Ficha do Veículo:", dados);
        }

        [Fact]
        public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
        {
            // Arrange
            string nomeProprietario = "Ab";

            // Assert
            Assert.Throws<FormatException>(
                // Act
                () => new Veiculo(nomeProprietario)
            );
        }

        [Fact]
        public void TestaMensagemDeExcecaoDoQuartoCaractereDaPlaca()
        {
            // Arrange
            string placa = "ASDF8888";

            // Act
            var excecao = Assert.Throws<FormatException>(
                () => new Veiculo().Placa = placa
            );

            // Assert
            Assert.Equal("O 4° caractere deve ser um hífen", excecao.Message);
        }

        [Fact]
        public void TestaMensagemDeExcecaoDosUltimosCaracteresNumeros()
        {
            // Arrange
            string placa = "ABC-AAAA";

            // Act
            var excecao = Assert.Throws<FormatException>(
                () => new Veiculo().Placa = placa    
            );

            // Assert
            Assert.Equal("Do 5º ao 8º caractere deve-se ter um número!", excecao.Message);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado.");
        }
    }
}
