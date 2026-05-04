using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorsModels
{
    public class ModelStateErrors
    {
        public int StatusCode { get; set; } = 400;
        public string message { get; set; } = "one or more validation errors occurred";

        public IEnumerable<ValidationErrors> Errors { get; set; }
    }
}
