using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ViewModels
{
    public class ResultViewModel
    {
        public Boolean Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
