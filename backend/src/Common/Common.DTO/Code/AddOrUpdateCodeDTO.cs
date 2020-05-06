using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class AddOrUpdateCodeDTO
    {
        public string Name { get; set; }
        public bool? IsAlert { get; set; }
        public int GroupId { get; set; }
        public ICollection<string> Numbers { get; set; }
    }
}
