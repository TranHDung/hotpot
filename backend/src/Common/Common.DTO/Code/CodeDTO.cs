using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class CodeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public bool? IsAlert { get; set; }
        public int GroupId { get; set; }
        public ICollection<string> Numbers { get; set; }
        public int NotAppeareCount { get; set; }
        public int ReappeareCount { get; set; }
    }
}
