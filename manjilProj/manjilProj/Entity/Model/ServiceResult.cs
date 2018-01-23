using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Model
{
    public class ServiceResult<T> where T:class
    {
        public ServiceResult() { }
        public T Data { get; set; }
        public string Message { get; set; }
        public ResultStatus Status { get; set; }
    }

}
