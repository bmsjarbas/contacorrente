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
}