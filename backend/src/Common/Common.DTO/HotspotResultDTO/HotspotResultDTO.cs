using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTO
{
    public class HotspotResultDTO
    {
        public int Id { get; set; }
        public int DrawNumber { get; set; }

        public DateTime DrawDate { get; set; }

        public ICollection<string> BlueNumbers { get; set; }

        public string YellowNumber { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
