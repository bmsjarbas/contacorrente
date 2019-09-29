using System;
using System.Globalization;
using System.Runtime.Serialization;


namespace contacorrente.models
{
    [DataContract]
    public class LancamentoApi
    {
        [DataMember(Name = "data")]
        public string DataAsString { set; get; }
        [DataMember(Name = "descricao")]
        public string Descricao { get; set; }
        [DataMember(Name = "valor")]
        public string ValorAsString { get; set; }
        [DataMember(Name = "categoria")]
        public string Categoria { get; set; }

        [IgnoreDataMember]
        public DateTime Data
        {
            get
            {
                return DateTime.ParseExact(DataAsString, DataAsString.Contains(" ") ? "dd / MMM" : "dd/MMM", new CultureInfo("pt-BR"));
            }
        }

        public decimal Valor 
        {
            get 
            {
                return Decimal.Parse(ValorAsString.Replace(" ", ""), new CultureInfo("pt-BR"));
            }

        }

    }
}