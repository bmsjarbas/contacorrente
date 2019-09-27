using System;
using System.Collections.Generic;
using Xunit;

namespace tests
{
    public class ContaCorrenteTests
    {
        private List<Lancamento> lancamentos;
        private DateTime dataAntesDeOntem;
        private DateTime dataDeOntem;
        private DateTime dataHoje;

        public ContaCorrenteTests()
        {
            dataAntesDeOntem = DateTime.Now.AddDays(-2);
            dataDeOntem = DateTime.Now.AddDays(-1);
            dataHoje = DateTime.Now;
            var lancamentoAntesDeOntem = new Lancamento(dataAntesDeOntem, "Lançamento antes de ontem", 10, "alimentação");
            var lancamentoHoje = new Lancamento(dataHoje, "Lançamento hoje", 20, "ALIMENTAÇÃO");
            var lancamentoOntem = new Lancamento(dataDeOntem, "Lançamento ontem", 10, "transporte");

            lancamentos = new List<Lancamento> { lancamentoOntem, lancamentoAntesDeOntem, lancamentoHoje };
        }

        [Fact]
        public void DeveListarLancamentosOrdernadosPorData()
        {
            var contaCorrente = new ContaCorrente(lancamentos);
            var lancamentosOrdenados = contaCorrente.lancamentosOrdernadosPorData();
            Assert.Equal(dataHoje, lancamentosOrdenados[0].Data);
            Assert.Equal(dataDeOntem, lancamentosOrdenados[1].Data);
            Assert.Equal(dataAntesDeOntem, lancamentosOrdenados[2].Data);
        }

        [Fact]
        public void DeveListarGastosAgrupadosPorCategoria()
        {
            var contaCorrente = new ContaCorrente(lancamentos);
            var gastosAgrupadosPorCategoria = contaCorrente.listarGastosPorCategoria();
            Assert.Equal(2, gastosAgrupadosPorCategoria.Length);
            foreach (var categoria in gastosAgrupadosPorCategoria)
            {
                switch (categoria.Item1)
                {
                    case "alimentação": 
                        Assert.Equal(30, categoria.Item2);
                        break;
                    case "transporte":
                        Assert.Equal(10, categoria.Item2);
                        break;
                }
            }
        }
    }

}