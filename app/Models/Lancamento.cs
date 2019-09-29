using System;

namespace contacorrente.models
{
    public class Lancamento
    {
        public Lancamento(DateTime data, string descricao, decimal valor, string categoria = "")
        {
            Data = data;
            Descricao = descricao;
            Valor = valor;
            Categoria = categoria;
        }

        public DateTime Data { get; }
        public string Descricao { get; }
        public decimal Valor { get; }
        public string Categoria { get; }
    }
}