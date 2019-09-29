using System;
using contacorrente.parsers;
using Xunit;

namespace tests
{
    public class LogParserTests
    {
        LogParser logParser;
        public LogParserTests()
        {
            logParser = new LogParser();
        }
        [Fact]
        public void DadoUmaLinhaDeLogCompletaDeveRetornarObjetoLancamento()
        {
            var linha = "25-May        UATT                        -79,9               alimentacao";
            var lancamento = logParser.ExtrairLinhaLancamento(linha);
            Assert.Equal("25-May",lancamento.Data.ToString("dd-MMM"));
            Assert.Equal("UATT", lancamento.Descricao);
            Assert.Equal((decimal)-79.9D, lancamento.Valor, 2);
            Assert.Equal("alimentacao", lancamento.Categoria);
        }

        [Fact]
        public void DadoUmaLinhaDeLogSemCategoriaDeveRetornarObjetoLancamento()
        {
            var linha = "02-Jun        Jose Mota                   35	";
            var lancamento = logParser.ExtrairLinhaLancamento(linha);
            Assert.Equal("02-Jun",lancamento.Data.ToString("dd-MMM"));
            Assert.Equal("Jose Mota", lancamento.Descricao);
            Assert.Equal(35, lancamento.Valor, 2);
            Assert.Equal(string.Empty, lancamento.Categoria);
        }
    }
}
