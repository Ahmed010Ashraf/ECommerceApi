using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class InvalidPasswordException(string msg = "The provided password or email is incorrect.")
        : Exception(msg)
    {
    }
}
