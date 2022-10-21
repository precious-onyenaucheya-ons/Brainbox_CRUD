using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainbox.Core.DTO
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; } = false;
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ErrorItem> Error { get; set; }
    }

    public class ErrorItem
    {
        public string Description { get; set; }
        public string InnerException { get; set; }
    }
}
