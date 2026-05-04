using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PagenatedResponse<T>
    {
        public int PageSize {get; set;}
        public int PageIndex {get; set;}
        public int TotalCount {get; set;}

        public IEnumerable<T>Data { get; set;}
    }
}
