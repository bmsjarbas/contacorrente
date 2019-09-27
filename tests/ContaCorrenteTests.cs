using System;
using System.Collections.Generic;
using Xunit;

namespace tests
{
    public class ContaCorrenteTests
    {

        [Fact]
        public void DeveListarLancamentosOrdernadosPorData()
        {
            var dataAntesDeOntem = DateTime.Now.AddDays(-2);
            var dataDeOntem = DateTime.Now.AddDays(-1);
            var dataHoje = DateTime.Now;
            var lancamentoAntesDeOntem = new Lancamento(dataAntesDeOntem, "Lançamento antes de ontem", 10, "alimentação");
            var lancamentoHoje = new Lancamento(dataHoje, "Lançamento hoje", 20, "alimentação");
            var lancamentoOntem = new Lancamento(dataDeOntem, "Lançamento ontem", 10, "transporte");
            
            var lancamentos = new List<Lancamento>{lancamentoOntem, lancamentoAntesDeOntem, lancamentoHoje};
            var contaCorrente = new ContaCorrente(lancamentos);
            var lancamentosOrdenados = contaCorrente.lancamentosOrdernadosPorData();
            Assert.Equal(dataHoje, lancamentosOrdenados[0].Data);
            Assert.Equal(dataDeOntem, lancamentosOrdenados[1].Data);
            Assert.Equal(dataAntesDeOntem, lancamentosOrdenados[2].Data);
        }
    }

}