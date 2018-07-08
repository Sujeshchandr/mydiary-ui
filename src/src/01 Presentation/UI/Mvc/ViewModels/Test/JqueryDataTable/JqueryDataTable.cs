using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ViewModels.Test.JqueryDataTable
{
    public class JqueryDataTable
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }

        ////[JsonProperty("pageLength")]
        ////public int PageLength { get; set; }

        ////[JsonProperty("length")]
        ////public int Length { get; set; }

        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty("employees")]
        public IList<Employee> Data { get; set; }
    }

    public class Employee
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("salary")]
        public string Salary { get; set; }

        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("office")]
        public string Office { get; set; }

        [JsonProperty("extn")]
        public int Extension { get; set; }
    }
}