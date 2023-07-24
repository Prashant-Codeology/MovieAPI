﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public  class Respo <T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public HttpStatusCode HttpStatus { get; set; }
        public T Data { get; set; }
    }
}
