using System;
using contacorrente.models;
using Xunit;

public class LancamentoTests
{

    [Fact]
    public void DeveNormalizarNomeCategoriaComoLowerCase()
    {
        var lancamento = new Lancamento(DateTime.Now, "descrição", 10, "CATEGORIA");
        Assert.Equal("categoria", lancamento.Categoria);
    }

    [Fact]
    public void DeveNormalizarNomeCategoriaRemovendoAcentuacao()
    {
        var lancamento = new Lancamento(DateTime.Now, "descrição", 10, "ALIMENTAÇÃO");
        Assert.Equal("alimentacao", lancamento.Categoria);
    }
}