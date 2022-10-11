using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HawkMiddlewares.Data
{
    public class XAuthRequest<T> where T : class
    {
        public XAuthRequest()
        {
            Params = default;
        }

        public string AppId { get; set; }
        public T Params { get; set; }

        public int ItemCount { get; set; }

        [JsonIgnore()]
        public bool IsHasParams
        {
            get
            {
                return Params != null;
            }
        }

    }
}
