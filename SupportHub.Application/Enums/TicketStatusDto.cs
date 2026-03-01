using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Enums
{
    public enum TicketStatusDto { Open, InProgress, Resolved, Closed }
    public enum TicketPriorityDto { Low, Medium, High, Urgent }
    public enum TicketCategoryDto { Hardware, Software, Network, Other }
}
