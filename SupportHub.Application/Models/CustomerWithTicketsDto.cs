using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Models
{
    public class CustomerWithTicketsDto : CustomerDto
    {
        public ICollection<TicketDto> Tickets { get; set; } = new List<TicketDto>();
    }
}
