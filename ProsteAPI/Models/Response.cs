using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProsteAPI.Models
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
#nullable enable
        public T? Object { get; set; }
#nullable disable

    }
}
