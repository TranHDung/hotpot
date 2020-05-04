using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class GroupEmail: Entity
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Emails { get; set; }
    }
}
