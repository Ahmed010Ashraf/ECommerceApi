using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class badRequestException(List<string> errors) : Exception("errors occurred please check the errors list")
    {
        public List<string> Errors { get; } = errors;
    }
}
