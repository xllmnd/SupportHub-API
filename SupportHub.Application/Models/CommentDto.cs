using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Models
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime CreateAt { get; set; }

        public int? AgentId { get; set; }
        public string? AgentName { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
    }
}
