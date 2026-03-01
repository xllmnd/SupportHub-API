using SupportHub.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Models
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        
        public TicketStatusDto Status { get; set; }
        public TicketPriorityDto Priority { get; set; }
        public TicketCategoryDto Category { get; set; }

        public string CustomerName { get; set; } = null!;
        public string? AssignedAgentName { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
