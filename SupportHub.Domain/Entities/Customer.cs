using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Domain.Entities
{
    public class Customer:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
