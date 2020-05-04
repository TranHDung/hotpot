using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Code: Entity
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string NumbersString { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int NotAppeareCount { get; set; }

        [Required]
        public int ReappeareCount { get; set; }

        public bool? IsAlert { get; set; }

        public ICollection<WonCode> WonCodes { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }

        [NotMapped]
        public ICollection<string> Numbers
        {
            get
            {
                return this.NumbersString.Trim(',').Split(',');
            }
            set
            {
                this.NumbersString = value.Count > 0 ? string.Join(",", value) : "";
            }
        }
    }
}
