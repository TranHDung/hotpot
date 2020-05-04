using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Option: Entity
    {
        [Required]
        [StringLength(500)]
        public string Key { get; set; }

        [Required]
        [StringLength(500)]
        public string Value { get; set; }
    }
}
