using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class FilterGroup
    {
        public string name { get; set; }
        public Sorting Sorting { get; set; }
        public Paging Paging { get; set; }
    }
}
