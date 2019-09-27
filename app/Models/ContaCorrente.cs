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
        var lancamentosAgrupados = from lanc in Lancamentos
                                    group lanc.Valor by lanc.Categoria.ToLower() into g
                                    select  Tuple.Create(g.Key, g.Sum());
        return lancamentosAgrupados.ToArray();
    }
}
        