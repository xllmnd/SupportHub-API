using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Domain.Entities
{
    public class Comment:BaseEntity
    {
        public int TicketId { get; set; }
        public int? AgentId { get; set; }
        public int? CustomerId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public Ticket Ticket { get; set; } = null!;
        public Agent? Agent { get; set; }
        public Customer? Customer { get; set; }
    }
}
