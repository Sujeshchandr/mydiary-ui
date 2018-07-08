using MyDiary.Domain.Domain.Abstracts;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyDiary.Domain.Domains
{
    [ComplexType]
    public class Chart :IChart
    {
        [JsonProperty("seqNumber")]
        public int SeqNumber { get; set; }


        [JsonProperty("amount")]
        public float Amount { get; set; }
    }
}