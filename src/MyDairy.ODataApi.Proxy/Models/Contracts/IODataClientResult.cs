using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.ODataApi.Proxy.Models.Contracts
{
    public interface IODataClientResult<T>
    {
        [JsonProperty(PropertyName = "@odata.context")]
        Uri ODataContext { get; set; }

        T Value { get; set; }

    }
}
