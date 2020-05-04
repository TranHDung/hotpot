using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Entities
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public void Created() 
        {
            var now = DateTime.UtcNow;
            CreateAt = now;
            ModifiedAt = now;

        }

        public void Modified()
        {
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
