using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Models
{
    public class CreateTicketDto
    {
        public int CustomerId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Category { get; set; } 
        public int Priority { get; set; }
    }
}
