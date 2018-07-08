using MyDiary.ODataApi.Proxy.Models.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.ODataApi.Proxy.Models
{
    public class ODataClientResult<T> : IODataClientResult<T>
    {
        [JsonProperty(PropertyName = "@odata.context")]
        Uri IODataClientResult<T>.ODataContext { get; set; }

        public T Value { get; set; }
    }
}