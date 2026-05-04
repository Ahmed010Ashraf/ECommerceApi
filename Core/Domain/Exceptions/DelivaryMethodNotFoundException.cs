using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DelivaryMethodNotFoundException(Guid id ):NotFoundException($"delivary method with this id {id} is not found")
    {
    }
}
