using System;
using System.Collections.Generic;
using System.Linq;

namespace contacorrente.models
{
    public class ContaCorrente
    {
        private List<Lancamento> _lancamentos;
        public ContaCorrente(List<Lancamento> lancamentos)
        {
            _lancamentos = lancamentos;
        }

        public IEnumerable<Lancamento> Lancamentos { get { return _lancamentos;} }

        public List<Lancamento> lancamentosOrdernadosPorData()
        {
            return new List<Lancamento>(_lancamentos.OrderByDescending(x => x.Data));
        }

        public Tuple<string, decimal>[] listarGastosPorCategoria()
        {
            return lancamentosAgrupadosPorCategoria().ToArray();
        }

        public Tuple<string, decimal> CategoriaComMaiorGasto() => lancamentosAgrupadosPorCategoria().OrderByDescending(x => x.Item2).First();

        private IEnumerable<Tuple<string, decimal>> lancamentosAgrupadosPorCategoria()
        {
            var lancamentosAgrupados = from lanc in _lancamentos
                                       where lanc.Valor < 0
                                       group lanc.Valor by lanc.Categoria into g
                                       select Tuple.Create(g.Key, Math.Abs(g.Sum()));
            return lancamentosAgrupados;
        }

        public Tuple<string, decimal> MesComMaiorGasto()
        {
            var lancamentosAgrupados = from lanc in _lancamentos
                                       where lanc.Valor < 0
                                       group lanc.Valor by lanc.Data.ToString("MMM") into g
                                       select Tuple.Create(g.Key, Math.Abs(g.Sum()));
            return lancamentosAgrupados.OrderByDescending(x => x.Item2).First();
        }

        public decimal TotalDeGastos()
        {
            var valorTotal = _lancamentos.Where(x => x.Valor < 0).Sum(x => x.Valor);
            return Math.Abs(valorTotal);
        }

        public decimal TotalDeRecebimentos()
        {
            var valorTotal = _lancamentos.Where(x => x.Valor > 0).Sum(x => x.Valor);
            return Math.Abs(valorTotal);
        }

        public decimal SaldoDeMovimentacoes()
        {
            return _lancamentos.Sum(x => x.Valor);
        }

        public void IncluirMovimentacaoEmLote(List<Lancamento> novosLancamentos)
        {
            _lancamentos.AddRange(novosLancamentos);
        }
    }
}
