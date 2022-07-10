using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class JDO<T>
    {
        [JsonProperty("odata.metadata")]
        public string context { get; set; }
        public List<T> value { get; set; }
    }
}
