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


            string operacao = "";
            while (operacao != "0")
            {
                ImprimirMenuInicial();
                operacao = Console.ReadLine();
                switch (operacao)
                {
                    case "0":
                        break;
                    case "1":
                        ImprimirMovimentacaoOrdenada(contaCorrente.lancamentosOrdernadosPorData());
                        PressioneParaContinuar();
                        continue;
                    case "2":
                        ImprimirTotalDeGastosPorCategoria(contaCorrente.listarGastosPorCategoria());
                        PressioneParaContinuar();
                        continue;
                    case "3":
                        ImprimirTuplaItemValor(contaCorrente.CategoriaComMaiorGasto());
                        PressioneParaContinuar();
                        continue;
                    case "4":
                        ImprimirTuplaItemValor(contaCorrente.MesComMaiorGasto());
                        PressioneParaContinuar();
                        continue;
                    case "5":
                        ImprimirTotalDeGastos(contaCorrente.TotalDeGastos());
                        PressioneParaContinuar();
                        continue;
                    case "6":
                        ImprimirTotalDeRecebimentos(contaCorrente.TotalDeRecebimentos());
                        PressioneParaContinuar();
                        continue;
                     case "7":
                        ImprimirSaldo(contaCorrente.SaldoDeMovimentacoes());
                        PressioneParaContinuar();
                        continue;
                    

                }
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

        private static void ImprimirMenuInicial()
        {
            Console.WriteLine("1 - exibir o log de movimentações de forma ordenada");
            Console.WriteLine("2 - informar o total de gastos por categoria");
            Console.WriteLine("3 - informar qual categoria cliente gastou mais");
            Console.WriteLine("4 - informar qual foi o mês que cliente mais gastou");
            Console.WriteLine("5 - quanto de dinheiro o cliente gastou");
            Console.WriteLine("6 - quanto de dinheiro o cliente recebeu");
            Console.WriteLine("7 - saldo total de movimentações do cliente");
            Console.WriteLine("0 - Sair");
        }

        private static void ImprimirMovimentacaoOrdenada(List<Lancamento> lancamentos)
        {
            foreach (var lancamento in lancamentos)
            {
                Console.WriteLine($"{lancamento.Data:dd-MMM}\t{lancamento.Descricao.PadRight(28, ' ')}{lancamento.Valor:N2}\t\t\t{lancamento.Categoria}");
            }

        }

        private static void PressioneParaContinuar()
        {
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadLine();
        }
        private static void ImprimirTotalDeGastosPorCategoria(Tuple<string, decimal>[] gastosPorCategoria)
        {
            foreach (var item in gastosPorCategoria)
            {
                ImprimirTuplaItemValor(item);
            }
        }
                private static void ImprimirSaldo(decimal valor)
        {
            Console.WriteLine($"Saldo movimentacoes: {valor:N2}");
        }

        private static void ImprimirTotalDeRecebimentos(decimal valor)
        {
            Console.WriteLine($"Total de Recebimentos: {valor:N2}");
        }

        private static void ImprimirTotalDeGastos(decimal valor)
        {
            Console.WriteLine($"Total de Gastos: {valor:N2}");
        }

        private static void ImprimirTuplaItemValor(Tuple<string, decimal> tupla)
        {
            Console.WriteLine($"{tupla.Item1.PadRight(20)}\t{tupla.Item2:N2}");
        }


    }

}