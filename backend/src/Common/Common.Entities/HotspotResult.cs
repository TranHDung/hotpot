using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class HotspotResult: Entity
    {
        [Required]
        public int DrawNumber { get; set; }

        [Required]
        public DateTime DrawDate { get; set; }

        [Required]
        [StringLength(500)]
        public string BlueString { get; set; }

        [Required]
        [StringLength(5)]
        public string YellowNumber { get; set; }

        public ICollection<WonCode> WonCodes { get; set; }

        [NotMapped]
        public ICollection<string> BlueNumbers
        {
            get
            {
                return this.BlueString.Trim(',').Split(',');
            }
            set
            {
                this.BlueString = value.Count > 0 ? string.Join(",", value) : "";
            }
        }
    }
}
