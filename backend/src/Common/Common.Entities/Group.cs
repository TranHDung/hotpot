using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Group: Entity
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public ICollection<Code> Codes { get; set; }
    }
}
