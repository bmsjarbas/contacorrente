using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using contacorrente.models;
using contacorrente.parsers;
using contacorrente.repositories;

namespace contacorrente.app
{
    class Program
    {
        private static ContaCorrente _contaCorrente;
        static void Main(string[] args)
        {
            if (args.Count() == 0)
                throw new ArgumentException("Erro - Informar arquivo de log inicial");
            var pathArquivoLogInicial = args[0];
            var contaCorrente = IniciarContaCorrente(pathArquivoLogInicial);
            var lancamentosApi = RecuperarLancamentosApi();
            contaCorrente.IncluirMovimentacaoEmLote(lancamentosApi);

            while(Console.ReadLine() != "0")
            {
                
            }           

        }

        private static List<Lancamento> RecuperarLancamentosApi()
        {
            using (var httpClient = new HttpClient())
            {
                var movimentacoes = new Movimentacoes(httpClient, @"https://my-json-server.typicode.com/cairano/backend-test", "pagamentos", "recebimentos");
                var lancamentos = new List<Lancamento>();
                lancamentos.AddRange(movimentacoes.ListarPagamentos());
                lancamentos.AddRange(movimentacoes.ListarRecebimentos());
                return lancamentos;
            }


        }


        private static ContaCorrente IniciarContaCorrente(string pathArquivoLogInicial)
        {
            FileStream fileStream = new FileStream(pathArquivoLogInicial, FileMode.Open);
            var lancamentosArquivo = ExtrairLancamentosDoArquivoDeLog(fileStream);
            return new ContaCorrente(lancamentosArquivo);
        }


        private static List<Lancamento> ExtrairLancamentosDoArquivoDeLog(Stream fileStream)
        {
            var logParser = new LogParser();
            var lancamentos = new List<Lancamento>();
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string cabecalho = reader.ReadLine();
                string linhaAtual;
                while ((linhaAtual = reader.ReadLine()) != null)
                {
                    var lancamento = logParser.ExtrairLinhaLancamento(linhaAtual);
                    lancamentos.Add(lancamento);
                }
            }

            return lancamentos;
        }

    }

}