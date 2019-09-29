using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using contacorrente.models;

namespace contacorrente.parsers
{
    public class LogParser
    {

        public Lancamento ExtrairLinhaLancamento(string linhaAtual)
        {
            var dataInicialTamanho = 14;
            var descricaoTamanho = 28;
            var valorTamanho = 12;

            var dataTransacaoAsString = linhaAtual.Substring(0, dataInicialTamanho).Trim();
            var descricao = linhaAtual.Substring(dataInicialTamanho, descricaoTamanho).Trim();
            var valorAsString = linhaAtual.Substring(dataInicialTamanho + descricaoTamanho);
            string categoria = "";
            if (valorAsString.Count() > 20)
            {
                valorAsString = valorAsString.Substring(0, valorTamanho).Trim();
                categoria = linhaAtual.Substring(dataInicialTamanho + descricaoTamanho + valorTamanho - 1).Trim();
            }
            var dataTransacao = DateTime.ParseExact(dataTransacaoAsString, "dd-MMM", CultureInfo.InvariantCulture);
            var valorTransacao = Decimal.Parse(valorAsString.Trim(), new CultureInfo("pt-BR"));

            return new Lancamento(dataTransacao, descricao, valorTransacao, categoria.ToLower());

        }

    }
}