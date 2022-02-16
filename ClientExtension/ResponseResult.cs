using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientExtension
{
    public class ResponseResult<T>
    {
        public ResponseResult()
        {
        }

        public ResponseResult(T body, HttpResponseMessage httpResponseMessage)
        {
            Body = body;
            HttpResponseMessage = httpResponseMessage;
        }

        public T Body { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}
