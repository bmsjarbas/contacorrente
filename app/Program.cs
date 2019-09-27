using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 0)
                throw new ArgumentException("Erro - Informar arquivo de log inicial");
            var pathArquivoLogInicial = args[0];
            var fileStream = new FileStream(pathArquivoLogInicial, FileMode.Open);
            var lancamentos = ExtrairLancamentosDoArquivoDeLog(fileStream);

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