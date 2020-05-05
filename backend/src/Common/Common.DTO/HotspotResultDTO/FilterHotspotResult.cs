using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class FilterHotspotResult
    {
        public DateTime? StartDrawDate { get; set; }
        public DateTime? EndDrawDate { get; set; }
        public int? StartSession { get; set; }
        public int? EndSession { get; set; }
        public int? TopSeccion { get; set; }
        public int? GroupId { get; set; }
        public int? CodeId { get; set; }
        public Sorting Sorting { get; set; }
        public Paging Paging { get; set; }
    }
}
