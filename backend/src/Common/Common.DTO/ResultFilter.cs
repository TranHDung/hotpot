using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class ResultFilter<TEntity>
    {
        public ICollection<TEntity> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
