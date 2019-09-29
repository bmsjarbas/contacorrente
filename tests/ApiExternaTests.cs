using System.Net.Http;
using contacorrente.repositories;
using Xunit;

public class ApiExternaTests
{
    [Fact]
    public void DeveChamarApiExternaERetornarObjetosPagamentos()
    {
        using(var httpClient = new HttpClient())
        {
             var movimentacoes = new Movimentacoes(httpClient, "https://my-json-server.typicode.com/cairano/backend-test", "recebimentos", "pagamentos");
             var lancamentos = movimentacoes.ListarPagamentos();
             Assert.NotEmpty(lancamentos);
        }
       
    }
}