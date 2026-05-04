using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryPatameter
    {
        public int? Brandid { get; set; }
        public int? Typeid { get; set; }
        public ProductSortingOptions? ProductSortingOptions { get; set; }

        public string? SearchValue { get; set; }

        private int _defaultvalue = 5;
        private int _maxsize = 10;

        private int _pagesize;

        public int pagesize
        {
            get { return _pagesize>0?_pagesize:_defaultvalue; }
            set { _pagesize = value > 0 && value < _maxsize ? value : _defaultvalue; }
        }

      

        public int pageindex { get; set; } = 1;
       
    }
}
