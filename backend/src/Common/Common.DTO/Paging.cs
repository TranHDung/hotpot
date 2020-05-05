using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class Paging
    {
        public int Skip { get; set; } = 0;
        public int Top { get; set; } = 10;
    }
}
