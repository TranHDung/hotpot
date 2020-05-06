using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class FilterCode
    {
        public int? StartNotAppeareCount { get; set; }
        public int? EndNotAppeareCount { get; set; }
        public int? NotAppeareCount { get; set; }
        public string CodeName { get; set; }
        public string GroupName { get; set; }
        public Sorting Sorting { get; set; }
        public Paging Paging { get; set; }
    }
}
