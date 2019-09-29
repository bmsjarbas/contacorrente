using System;
using System.Collections.Generic;
using System.Linq;
using contacorrente.models;
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
            var lancamentoAntesDeOntem = new Lancamento(dataAntesDeOntem, "Lançamento antes de ontem", -10, "alimentação");
            var lancamentoHoje = new Lancamento(dataHoje, "Lançamento hoje", -20, "alimentação");
            var lancamentoOntem = new Lancamento(dataDeOntem, "Lançamento ontem", -10, "transporte");

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

        [Fact]
        public void DeveRetornarCategoriaComMaiorGasto()
        {
            var contaCorrente = new ContaCorrente(lancamentos);
            var categoriaComMaiorGasto = contaCorrente.CategoriaComMaiorGasto();
            Assert.Equal("alimentação", categoriaComMaiorGasto.Item1);
            Assert.Equal(30, categoriaComMaiorGasto.Item2);

        }

        [Fact]
        public void DeveRetornarMesComMaiorGasto()
        {
            var dataMesRetrasado = DateTime.Now.AddMonths(-2);
            var dataMesPassado = DateTime.Now.AddMonths(-1);
            var dataMesAtual = DateTime.Now;

            var lancamentoMesRetrasado = new Lancamento(dataMesRetrasado, "Lançamento mês retrasado", -10, "alimentação");
            var lancamentoMesPassado = new Lancamento(dataMesPassado, "Lançamento mês passado", -20, "alimentação");
            var lancamentoMesAtual = new Lancamento(dataMesAtual, "Lançamento mês atual", -10, "transporte");
            var outroLancamentoMesAtual = new Lancamento(dataMesAtual, "Lançamento mês atual", -30, "alimentação");

            var lancamentosPorMes = new List<Lancamento>{lancamentoMesAtual, lancamentoMesPassado, lancamentoMesRetrasado, outroLancamentoMesAtual};
            var contaCorrente = new ContaCorrente(lancamentosPorMes);

            var mesComMaiorGasto = contaCorrente.MesComMaiorGasto();
            Assert.Equal(dataMesAtual.ToString("MMM"), mesComMaiorGasto.Item1);;


        }

        [Fact]
        public void DeveRetornarTotalDeGastos(){
            var contaCorrente = new ContaCorrente(lancamentos);
            decimal totalDeGastos = contaCorrente.TotalDeGastos();
            Assert.Equal(40, totalDeGastos);
        }

        [Fact]
        public void DeveRetornarTotalDeRecebimentos(){
            var recebimentoAntesDeOntem = new Lancamento(dataAntesDeOntem, "Depósito", 10);
            var recebimentoHoje = new Lancamento(dataHoje, "Transferência", 20);
            var recebimentoOntem = new Lancamento(dataDeOntem, "Depósito", 20);

            var lancamentosDeRecebimento = new List<Lancamento> { recebimentoAntesDeOntem, recebimentoHoje, recebimentoOntem };
            var contaCorrente = new ContaCorrente(lancamentosDeRecebimento);
            decimal totalDeRecebimentos = contaCorrente.TotalDeRecebimentos();
            Assert.Equal(50, totalDeRecebimentos);
        }

         [Fact]
        public void DeveRetornarSaldoDeMovimentacoes(){
            var recebimentoAntesDeOntem = new Lancamento(dataAntesDeOntem, "Depósito", 10);
            var recebimentoHoje = new Lancamento(dataHoje, "Transferência", 20);
            var recebimentoOntem = new Lancamento(dataDeOntem, "Depósito", 20);

            var lancamentosDeRecebimento = new List<Lancamento> { recebimentoAntesDeOntem, recebimentoHoje, recebimentoOntem };
            lancamentos.AddRange(lancamentosDeRecebimento);
            var contaCorrente = new ContaCorrente(lancamentos);
            decimal totalDeMovimentacoes = contaCorrente.SaldoDeMovimentacoes();
            Assert.Equal(10, totalDeMovimentacoes);
        }

        [Fact]
        public void DeveIncluirMovimentacaoEmLote()
        {
            var contaCorrente = new ContaCorrente(lancamentos);
            Assert.Equal(3, contaCorrente.Lancamentos.Count());
            var recebimentoAntesDeOntem = new Lancamento(dataAntesDeOntem, "Depósito", 10);
            var recebimentoHoje = new Lancamento(dataHoje, "Transferência", 20);
            var recebimentoOntem = new Lancamento(dataDeOntem, "Depósito", 20);
            var lancamentosDeRecebimento = new List<Lancamento> { recebimentoAntesDeOntem, recebimentoHoje, recebimentoOntem };
            contaCorrente.IncluirMovimentacaoEmLote(lancamentosDeRecebimento);
            Assert.Equal(6, contaCorrente.Lancamentos.Count());
        }
    }

}