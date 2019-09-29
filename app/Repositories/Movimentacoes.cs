
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using contacorrente.models;

namespace contacorrente.repositories
{
    public class Movimentacoes
    {
        private readonly HttpClient httpClient;
        private readonly string uriPagamentos;
        private readonly string uriRecebimentos;

        public Movimentacoes(HttpClient httpClient, string baseUri, string recebimentosResource, string pagamentosResource)
        {
            this.httpClient = httpClient;
            this.uriPagamentos = $"{baseUri}/{pagamentosResource}";
            this.uriRecebimentos = $"{baseUri}/{recebimentosResource}";
        }

        public List<Lancamento> ListarPagamentos()
        {
            
            var result = ProcessarHttpRequestAsync(uriPagamentos).Result;
            return MontarListaLancamentos(result);
            
        }

        public List<Lancamento> ListarRecebimentos()
        {
            var result = ProcessarHttpRequestAsync(uriRecebimentos).Result;
            return MontarListaLancamentos(result);
        }

        private async Task<List<LancamentoApi>> ProcessarHttpRequestAsync(string uri)
        {
            try
            {   

                var serialiador = new DataContractJsonSerializer(typeof(List<LancamentoApi>));
                var streamTask = httpClient.GetStreamAsync(uri);
                var lancamentosApi = serialiador.ReadObject(await streamTask) as List<LancamentoApi>;
                return lancamentosApi;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nTivemos um problema!");
                Console.WriteLine("Mensagem :{0} ", e.Message);
            }
            return null;
        }

        private Lancamento ConverterApiModelToModel(LancamentoApi apiModel)
        {
            return new Lancamento(apiModel.Data, apiModel.Descricao, apiModel.Valor, apiModel.Categoria?.ToLower() ?? string.Empty);
        }

        private List<Lancamento> MontarListaLancamentos(List<LancamentoApi> lancamentosApi)
        {
            var lancamentos = new List<Lancamento>();
            lancamentosApi.ForEach(apiModel => lancamentos.Add(ConverterApiModelToModel(apiModel)));
            return lancamentos;
        }
    }
}