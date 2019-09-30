using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace contacorrente.models
{
    public class Lancamento
    {
        public Lancamento(DateTime data, string descricao, decimal valor, string categoria = "")
        {
            Data = data;
            Descricao = descricao;
            Valor = valor;
            Categoria = NormalizarCategoria(categoria);
        }

        public DateTime Data { get; }
        public string Descricao { get; }
        public decimal Valor { get; }
        public string Categoria { get; }


        private string NormalizarCategoria(string texto)
        {
            if(string.IsNullOrEmpty(texto))
                return "";
                
            var decomposed = texto.Normalize(NormalizationForm.FormD);
            var filtered = decomposed.Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
            return new String(filtered.ToArray()).ToLower();
        }
    }
}