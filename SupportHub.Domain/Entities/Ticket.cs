using SupportHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Domain.Entities
{
    public class Ticket:BaseEntity
    {
        public int CustomerId { get; set; }
        public int? AssignedAgentId { get; set; }
        public TicketCategory Category { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Customer Customer { get; set; } = null!;
        public Agent? AssignedAgent { get; set; }
        
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
