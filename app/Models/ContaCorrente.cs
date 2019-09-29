using System;
using System.Collections.Generic;
using System.Linq;

public class ContaCorrente
{
    public ContaCorrente(List<Lancamento> lancamentos)
    {
        Lancamentos = lancamentos;
    }

    public IEnumerable<Lancamento> Lancamentos { get; }

    public List<Lancamento> lancamentosOrdernadosPorData()
    {
        return new List<Lancamento>(Lancamentos.OrderByDescending(x=>x.Data));
    }

    public Tuple<string, decimal>[] listarGastosPorCategoria()
    {
        return lancamentosAgrupadosPorCategoria().ToArray();
    }

    public Tuple<string, decimal> CategoriaComMaiorGasto() => lancamentosAgrupadosPorCategoria().OrderByDescending(x => x.Item2).First();

    private IEnumerable<Tuple<string, decimal>> lancamentosAgrupadosPorCategoria()
     {
         var lancamentosAgrupados = from lanc in Lancamentos
                                    where lanc.Valor < 0
                                    group lanc.Valor by lanc.Categoria.ToLower() into g
                                    select  Tuple.Create(g.Key, Math.Abs(g.Sum()));
        return lancamentosAgrupados;
     }

    public Tuple<string, decimal> MesComMaiorGasto()
    {
        var lancamentosAgrupados = from lanc in Lancamentos
                                    where lanc.Valor < 0
                                    group lanc.Valor by lanc.Data.ToString("MMM") into g
                                    select  Tuple.Create(g.Key, Math.Abs(g.Sum()));
        return lancamentosAgrupados.OrderByDescending(x=>x.Item2).First();
    }

    public decimal TotalDeGastos()
    {
        var valorTotal = Lancamentos.Where(x=>x.Valor < 0).Sum(x=>x.Valor);
        return Math.Abs(valorTotal);
    }

    public decimal TotalDeRecebimentos()
    {
        var valorTotal = Lancamentos.Where(x=>x.Valor > 0).Sum(x=>x.Valor);
        return Math.Abs(valorTotal);
    }

    public decimal SaldoDeMovimentacoes()
    {
        return Lancamentos.Sum(x=>x.Valor);
    }
}
        