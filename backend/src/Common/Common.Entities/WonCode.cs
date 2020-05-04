using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class WonCode: Entity
    {
        [Required]
        public int Deficit { get; set; }

        public int HotspotResultId { get; set; }

        public HotspotResult HotspotResult { get; set; }

        public int CodeId { get; set; }

        public Code Code { get; set; }
    }
}
