using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes
{
    public class PatioTestes : IDisposable
    {
        private Veiculo veiculo;
        private Patio estacionamento;
        private Operador operador;
        private ITestOutputHelper SaidaConsoleTeste;

        public PatioTestes(ITestOutputHelper _saidaConsoleTeste)
        {
            veiculo = new Veiculo();
            estacionamento = new Patio();
            operador = new Operador();
            operador.Nome = "Pedro Fagundes";
            SaidaConsoleTeste = _saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor invocado.");
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculoDoTipoAutomovel()
        {
            //Arrange
            veiculo.Proprietario = "José Silva";
            veiculo.Tipo = TipoVeiculo.Automovel;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "ASD-9999";

            estacionamento.OperadorPatio = operador;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Fact]
        public void ValidaFaturamentoDoEstacionamentoComUmVeiculoDoTipoMotocicleta()
        {
            //Arrange
            veiculo.Proprietario = "José Silva";
            veiculo.Tipo = TipoVeiculo.Motocicleta;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "ASD-9999";

            estacionamento.OperadorPatio = operador;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(1, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        [InlineData("Jose Silva", "PQL-9242", "Cinza", "Fusca")]
        [InlineData("Maria Silva", "GDR-6524", "Azul", "Opala")]
        public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            veiculo.Proprietario = proprietario;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.OperadorPatio = operador;
            estacionamento.RegistrarEntradaVeiculo(veiculo);
            estacionamento.RegistrarSaidaVeiculo(veiculo.Placa);

            //Act
            double faturamento = estacionamento.TotalFaturado();

            //Assert
            Assert.Equal(2, faturamento);
        }

        [Theory]
        [InlineData("André Silva", "ASD-1498", "Preto", "Gol")]
        public void LocalizaVeiculoNoPatioComBaseNoIdTicket(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            veiculo.Proprietario = proprietario;
            veiculo.Tipo = TipoVeiculo.Motocicleta;
            veiculo.Cor = cor;
            veiculo.Modelo = modelo;
            veiculo.Placa = placa;

            estacionamento.OperadorPatio = operador;
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = estacionamento.PesquisaVeiculo(veiculo.IdTicket);

            //Assert
            Assert.Contains("### Ticket Estacionamento Alura ###", consultado.Ticket);
        }

        [Fact]
        public void TestaAlteracaoDaCorDoProprioVeiculo()
        {
            //Arrange
            veiculo.Proprietario = "José Silva";
            veiculo.Tipo = TipoVeiculo.Motocicleta;
            veiculo.Cor = "Verde";
            veiculo.Modelo = "Fusca";
            veiculo.Placa = "ASD-9999";
            
            estacionamento.OperadorPatio = operador;
            estacionamento.RegistrarEntradaVeiculo(veiculo);

            var veiculoAlterado = new Veiculo()
            {
                Proprietario = "José Silva",
                Tipo = TipoVeiculo.Motocicleta,
                Cor = "Preto",
                Modelo = "Fusca",
                Placa = "ASD-9999"
            };

            //Act
            Veiculo alterado = estacionamento.AlteraDadosVeiculo(veiculoAlterado);

            //Assert
            Assert.Equal(alterado.Cor, veiculoAlterado.Cor);
        }

        public void Dispose()
        {
            SaidaConsoleTeste.WriteLine("Dispose invocado.");
        }
    }
}
