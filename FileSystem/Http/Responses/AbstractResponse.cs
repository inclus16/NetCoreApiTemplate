using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Http.Responses
{
    public class AbstractResponse
    {
        public bool Success = true;

        public object Data { get; set; }
    }
}
